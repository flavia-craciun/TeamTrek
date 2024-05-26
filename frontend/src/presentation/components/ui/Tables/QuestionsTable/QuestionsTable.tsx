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
import { useQuestionTableController } from "./QuestionsTable.controller";
import { QuestionDTO, QuestionUpdateDTO } from "@infrastructure/apis/client";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import SearchIcon from "@mui/icons-material/Search";
import { QuestionAddDialog } from "../../Dialogs/QuestionAddDialog";
import { QuestionEditDialog } from "../../Dialogs/QuestionEditDialog";
import { RemovalDialog } from "../../Dialogs/RemovalDialog";
import { useAppSelector } from "@application/store";
import { format } from "date-fns";

const useHeader = (): { key: string; name: string }[] => {
    const { formatMessage } = useIntl();

    return [
        { key: "title", name: formatMessage({ id: "globals.questionTitle" }) },
        { key: "description", name: formatMessage({ id: "globals.description" }) },
        { key: "updatedAt", name: formatMessage({ id: "globals.modifiedAt" }) },
        { key: "answerCount", name: formatMessage({ id: "globals.answerCount" }) },
    ];
};

const getRowValues = (
    entries: QuestionDTO[] | null | undefined,
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

export const QuestionTable = () => {
    const { userId: ownQuestionId } = useAppSelector((x) => x.profileReducer);
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
        remove,
        getAnswers
    } = useQuestionTableController();
    const rowValues = getRowValues(pagedData?.data, orderMap);

    const [selectedQuestion, setSelectedQuestion] =
        useState<QuestionUpdateDTO | null>(null);
    const [selectedQuestionName, setSelectedQuestionName] = useState<string | null>(null);
    const [isEditDialogOpen, setEditDialogOpen] = useState(false);
    const [isRemovalDialogOpen, setRemovalDialogOpen] = useState(false);
    const [selectedQuestionId, setSelectedQuestionId] = useState<string | null>(null);
    const [searchQuery, setSearchQuery] = useState<string>("");
    const [filteredQuestions, setFilteredQuestions] = useState<QuestionDTO[] | null>(null);
    const [answerCounts, setAnswerCounts] = useState<{ [key: string]: number }>({});

    const fetchAnswerCounts = async () => {
        const counts: { [key: string]: number } = {};
        for (const question of filteredQuestions || []) {
            if (isUndefined(question.questionId))
                continue;
            const count = await getAnswers(question.questionId) 
            
            counts[question.questionId] = count ? count : 0;
        }
        setAnswerCounts(counts);
    };

    useEffect(() => {
        setFilteredQuestions(pagedData?.data ?? null);
    }, [pagedData]);

    useEffect(() => {
        const fetchAnswerCounts = async () => {
            const counts: { [key: string]: number } = {};
            for (const question of filteredQuestions || []) {
                if (!isUndefined(question.questionId)) {
                    const count = await getAnswers(question.questionId);
                    counts[question.questionId] = count ? count : 0;
                }
            }
            setAnswerCounts(counts);
        };

        fetchAnswerCounts();
    }, [filteredQuestions, getAnswers]);

    const handleEditClick = (question: QuestionUpdateDTO) => {
        setSelectedQuestion(question);
        setEditDialogOpen(true);
    };

    const handleEditDialogClose = () => {
        setEditDialogOpen(false);
    };

    const handleDeleteClick = (question: QuestionDTO) => {
        setSelectedQuestionName(question.title ? question.title : "");
        setSelectedQuestionId(question.questionId ? question.questionId : "");
        setRemovalDialogOpen(true);
    };

    const handleRemovalDialogClose = () => {
        setSelectedQuestionName(null);
        setSelectedQuestionId(null);
        setRemovalDialogOpen(false);
    };

    const handleConfirmRemoval = () => {
        console.log(selectedQuestionId);
        if (selectedQuestionId) {
            remove(selectedQuestionId);
            setRemovalDialogOpen(false);
        }
    };

    const handleSearchInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchQuery(event.target.value);
    };

    const handleSearchIconClick = () => {
        if (pagedData?.data) {
            const filtered = pagedData.data.filter((question) =>
                question.title?.toLowerCase().includes(searchQuery.toLowerCase()) ||
                question.description?.toLowerCase().includes(searchQuery.toLowerCase())
            );
            setFilteredQuestions(filtered);
        }
    };

    return (
        <DataLoadingContainer
            isError={isError}
            isLoading={isLoading}
            tryReload={tryReload}
        >
            <QuestionAddDialog />
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
                        {getRowValues(filteredQuestions, orderMap)?.map(({ data, entry }, rowIndex) => (
                            <TableRow key={`row_${rowIndex + 1}`}>
                                {data.map((keyValue, index) => (
                                    <TableCell key={`cell_${rowIndex + 1}_${index + 1}`}>
                                        {(() => {
                                            if (keyValue.key === "updatedAt") {
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
                                <TableCell>{answerCounts[!isUndefined(entry.questionId) ? entry.questionId : ""]}</TableCell>
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
                                        onClick={() => !isUndefined(entry.title) && handleDeleteClick(entry || "")}
                                    >
                                        <DeleteIcon color="error" fontSize="small" />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            {selectedQuestion && (
                <QuestionEditDialog
                    initialData={selectedQuestion}
                    isOpen={isEditDialogOpen}
                    onClose={handleEditDialogClose}
                />
            )}
            {<RemovalDialog
                objectName={selectedQuestionName ? selectedQuestionName : "this"}
                isOpen={isRemovalDialogOpen}
                onClose={handleRemovalDialogClose}
                onConfirm={handleConfirmRemoval}
            />}
        </DataLoadingContainer>
    );
};
