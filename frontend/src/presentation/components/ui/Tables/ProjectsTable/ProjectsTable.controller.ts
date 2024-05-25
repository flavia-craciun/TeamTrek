import { useTableController } from "../Table.controller";
import { useProjectApi } from "@infrastructure/apis/api-management";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useCallback } from "react";
import { usePaginationController } from "../Pagination.controller";
import { ProjectUpdateDTO } from "@infrastructure/apis/client";

/**
 * This is controller hook manages the table state including the pagination and data retrieval from the backend.
 */
export const useProjectTableController = () => {
    const { getProjects: { key: queryKey, query } ,  editProject: { key: editProjectKey, mutation: editProject }, deleteProject: { key: deleteProjectKey, mutation: deleteProject }} = useProjectApi(); // Use the API hook.
    const queryClient = useQueryClient(); // Get the query client.
    const { page, pageSize, setPagination } = usePaginationController(); // Get the pagination state.
    const { data, isError, isLoading } = useQuery({
        queryKey: [queryKey, page, pageSize],
        queryFn: () => query({ page, pageSize })
    });
    const { mutateAsync: deleteMutation } = useMutation({
        mutationKey: [deleteProjectKey],
        mutationFn: deleteProject
    }); // Use a mutation to remove an entry.
    const remove = useCallback(
        (id: string) => deleteMutation(id).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
        [queryClient, deleteMutation, queryKey]);
    const { mutateAsync: editMutation } = useMutation({
        mutationKey: [editProjectKey],
        mutationFn: editProject
    }); // Use a mutation to remove an entry.
    const edit = useCallback(
        (project: ProjectUpdateDTO) => editMutation(project).then(() => queryClient.invalidateQueries({ queryKey: [queryKey] })),
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