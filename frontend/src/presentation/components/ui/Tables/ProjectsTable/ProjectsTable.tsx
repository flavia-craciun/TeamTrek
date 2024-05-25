import React, { useState, useEffect } from "react";
import { useIntl } from "react-intl";
import { isUndefined } from "lodash";
import {
    IconButton,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TablePagination,
    TableRow,
    TextField
} from "@mui/material";
import { DataLoadingContainer } from "../../LoadingDisplay";
import { useProjectTableController } from "./ProjectsTable.controller";
import { ProjectDTO, ProjectUpdateDTO } from "@infrastructure/apis/client";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import SearchIcon from "@mui/icons-material/Search";
import { ProjectAddDialog } from "../../Dialogs/ProjectAddDialog";
import { ProjectEditDialog } from "../../Dialogs/ProjectEditDialog";
import { RemovalDialog } from "../../Dialogs/RemovalDialog";
import { useAppSelector } from "@application/store";
import { format } from "date-fns";

const useHeader = (): { key: keyof ProjectDTO; name: string }[] => {
    const { formatMessage } = useIntl();

    return [
        { key: "projectName", name: formatMessage({ id: "globals.projectName" }) },
        { key: "description", name: formatMessage({ id: "globals.description" }) },
        { key: "createdByUser", name: formatMessage({ id: "globals.addBy" }) },
        { key: "createdAt", name: formatMessage({ id: "globals.createdAt" }) },
    ];
};

const getRowValues = (
    entries: ProjectDTO[] | null | undefined,
    orderMap: { [key: string]: number }
) =>
    entries?.map((entry) => {
        return {
            entry: entry,
            data: Object.entries(entry)
                .filter(([e]) => !isUndefined(orderMap[e]))
                .sort(([a], [b]) => orderMap[a] - orderMap[b])
                .map(([key, value]) => {
                    return { key, value };
                }),
        };
    });

export const ProjectTable = () => {
    const { userId: ownProjectId } = useAppSelector((x) => x.profileReducer);
    const { formatMessage } = useIntl();
    const header = useHeader();
    const orderMap = header.reduce((acc, e, i) => {
        return { ...acc, [e.key]: i };
    }, {}) as { [key: string]: number };
    const {
        handleChangePage,
        handleChangePageSize,
        pagedData,
        isError,
        isLoading,
        tryReload,
        labelDisplay,
        remove
    } = useProjectTableController();
    const rowValues = getRowValues(pagedData?.data, orderMap);

    const [selectedProject, setSelectedProject] =
        useState<ProjectUpdateDTO | null>(null);
    const [selectedProjectName, setSelectedProjectName] = useState<string | null>(null);
    const [isEditDialogOpen, setEditDialogOpen] = useState(false);
    const [isRemovalDialogOpen, setRemovalDialogOpen] = useState(false);
    const [selectedProjectId, setSelectedProjectId] = useState<string | null>(null);
    const [searchQuery, setSearchQuery] = useState<string>("");
    const [filteredProjects, setFilteredProjects] = useState<ProjectDTO[] | null>(null);

    useEffect(() => {
        setFilteredProjects(pagedData?.data ?? null);
    }, [pagedData]);

    const handleEditClick = (project: ProjectUpdateDTO) => {
        setSelectedProject(project);
        setEditDialogOpen(true);
    };

    const handleEditDialogClose = () => {
        setEditDialogOpen(false);
    };

    const handleDeleteClick = (project: ProjectDTO) => {
        setSelectedProjectName(project.projectName ? project.projectName : "");
        setSelectedProjectId(project.projectId ? project.projectId : "");
        setRemovalDialogOpen(true);
    };

    const handleRemovalDialogClose = () => {
        setSelectedProjectName(null);
        setSelectedProjectId(null);
        setRemovalDialogOpen(false);
    };

    const handleConfirmRemoval = () => {
        console.log(selectedProjectId);
        if (selectedProjectId) {
            remove(selectedProjectId);
            setRemovalDialogOpen(false);
        }
    };

    const handleSearchInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(event.target.value);
    };

    const handleSearchIconClick = () => {
        if (pagedData?.data) {
            const filtered = pagedData.data.filter((project) =>
                project.projectName?.toLowerCase().includes(searchQuery.toLowerCase()) ||
                project.description?.toLowerCase().includes(searchQuery.toLowerCase())
            );
            setFilteredProjects(filtered);
        }
    };

    return (
        <DataLoadingContainer
            isError={isError}
            isLoading={isLoading}
            tryReload={tryReload}
        >
            <ProjectAddDialog />
            <div style={{ display: "flex", alignItems: "center", marginBottom: 10, marginTop: 10 }}>
                <TextField
                    style={{ width: 250 }}
                    placeholder="Search"
                    type="search"
                    value={searchQuery}
                    onChange={handleSearchInputChange}
                />
                <IconButton onClick={handleSearchIconClick}>
                    <SearchIcon />
                </IconButton>
            {!isUndefined(pagedData) &&
                !isUndefined(pagedData?.totalCount) &&
                !isUndefined(pagedData?.page) &&
                !isUndefined(pagedData?.pageSize) && (
                    <TablePagination
                        component="div"
                        count={pagedData.totalCount}
                        page={pagedData.totalCount !== 0 ? pagedData.page - 1 : 0}
                        onPageChange={handleChangePage}
                        rowsPerPage={pagedData.pageSize}
                        onRowsPerPageChange={handleChangePageSize}
                        labelRowsPerPage={formatMessage({ id: "labels.itemsPerPage" })}
                        labelDisplayedRows={labelDisplay}
                        showFirstButton
                        showLastButton
                    />
                )}
			 </div>	
			 <TableContainer component={Paper} style={{ height: '500px', width: '100%' }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            {header.map((e) => (
                                <TableCell key={`header_${String(e.key)}`}>{e.name}</TableCell>
                            ))}
                            <TableCell>{formatMessage({ id: "labels.edit" })}</TableCell>
                            <TableCell>{formatMessage({ id: "labels.delete" })}</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {getRowValues(filteredProjects, orderMap)?.map(({ data, entry }, rowIndex) => (
                            <TableRow key={`row_${rowIndex + 1}`}>
                                {data.map((keyValue, index) => (
                                    <TableCell key={`cell_${rowIndex + 1}_${index + 1}`}>
                                        {(() => {
                                            if (keyValue.key === "createdAt") {
                                                const dateValue = new Date(keyValue.value);
                                                return format(dateValue, "dd/MM/yyyy");
                                            } else if (
                                                typeof keyValue.value === "string" ||
                                                typeof keyValue.value === "number"
                                            ) {
                                                return keyValue.value;
                                            } else {
                                                return keyValue.value.name;
                                            }
                                        })()}
                                    </TableCell>
                                ))}
                                <TableCell>
                                    <IconButton
                                        color="primary"
                                        onClick={() => handleEditClick(entry)}
                                    >
                                        <EditIcon color="primary" fontSize="small" />
                                    </IconButton>
                                </TableCell>
                                <TableCell>
                                    <IconButton
                                        color="error"
                                        onClick={() => !isUndefined(entry.projectName) && handleDeleteClick(entry || "")}
                                    >
                                        <DeleteIcon color="error" fontSize="small" />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            {selectedProject && (
                <ProjectEditDialog
                    initialData={selectedProject}
                    isOpen={isEditDialogOpen}
                    onClose={handleEditDialogClose}
                />
            )}
            {<RemovalDialog
                objectName={selectedProjectName ? selectedProjectName : "this"}
                isOpen={isRemovalDialogOpen}
                onClose={handleRemovalDialogClose}
                onConfirm={handleConfirmRemoval}
            />}
        </DataLoadingContainer>
    );
};
