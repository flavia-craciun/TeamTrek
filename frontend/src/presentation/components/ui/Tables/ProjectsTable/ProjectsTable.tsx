import React, { useState } from "react";
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
} from "@mui/material";
import { DataLoadingContainer } from "../../LoadingDisplay";
import { useProjectTableController } from "./ProjectsTable.controller";
import { ProjectDTO, ProjectUpdateDTO } from "@infrastructure/apis/client";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { ProjectAddDialog } from "../../Dialogs/ProjectAddDialog";
import { ProjectEditDialog } from "../../Dialogs/ProjectEditDialog";
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
    } = useProjectTableController();
    const rowValues = getRowValues(pagedData?.data, orderMap);

    const [selectedProject, setSelectedProject] = useState<ProjectUpdateDTO | null>(null);
    const [isEditDialogOpen, setEditDialogOpen] = useState(false);

    const handleEditClick = (project: ProjectUpdateDTO) => {
        setSelectedProject(project);
        setEditDialogOpen(true);
    };

    const handleEditDialogClose = () => {
        setEditDialogOpen(false);
    };

    return (
        <DataLoadingContainer
            isError={isError}
            isLoading={isLoading}
            tryReload={tryReload}
        >
            <ProjectAddDialog />
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
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            {header.map((e) => (
                                <TableCell key={`header_${String(e.key)}`}>
                                    {e.name}
                                </TableCell>
                            ))}
                            <TableCell>{formatMessage({ id: "labels.edit" })}</TableCell>
                            <TableCell>{formatMessage({ id: "labels.delete" })}</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rowValues?.map(({ data, entry }, rowIndex) => (
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
                                    <IconButton color="error" onClick={() => ""}>
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
        </DataLoadingContainer>
    );
};
