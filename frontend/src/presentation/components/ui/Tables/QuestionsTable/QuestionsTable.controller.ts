import { useTableController } from "../Table.controller";
import { useQuestionApi } from "@infrastructure/apis/api-management";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useCallback } from "react";
import { usePaginationController } from "../Pagination.controller";
import { QuestionUpdateDTO } from "@infrastructure/apis/client";

/**
 * This is controller hook manages the table state including the pagination and data retrieval from the backend.
 */
export const useQuestionTableController = () => {
    const { getQuestions: { key: queryKey, query } ,  editQuestion: { key: editQuestionKey, mutation: editQuestion }, deleteQuestion: { key: deleteQuestionKey, mutation: deleteQuestion }} = useQuestionApi(); // Use the API hook.
    const queryClient = useQueryClient(); // Get the query client.
    const { page, pageSize, setPagination } = usePaginationController(); // Get the pagination state.
    const { data, isError, isLoading } = useQuery({
        queryKey: [queryKey, page, pageSize],
        queryFn: () => query({ page, pageSize })
    });
    const { mutateAsync: deleteMutation } = useMutation({
        mutationKey: [deleteQuestionKey],
        mutationFn: deleteQuestion
    }); // Use a mutation to remove an entry.
    const remove = useCallback(
        (id: string) => deleteMutation(id).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
        [queryClient, deleteMutation, queryKey]);
    const { mutateAsync: editMutation } = useMutation({
        mutationKey: [editQuestionKey],
        mutationFn: editQuestion
    }); // Use a mutation to remove an entry.
    const edit = useCallback(
        (question: QuestionUpdateDTO) => editMutation(question).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
        [queryClient, editMutation, queryKey]);

    const tryReload = useCallback(
        () => queryClient.invalidateQueries({ queryKey: [queryKey] }),
        [queryClient, queryKey]); 
    const tableController = useTableController(setPagination, data?.response?.pageSize);

    return {
        ...tableController,
        tryReload,
        pagedData: data?.response,
        isError,
        isLoading,
        edit,
        remove
    };
}