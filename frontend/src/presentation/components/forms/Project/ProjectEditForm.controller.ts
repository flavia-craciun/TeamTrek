import { ProjectEditFormController, ProjectEditFormModel } from "./ProjectEditForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useProjectApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";
import { ProjectUpdateDTO } from "@infrastructure/apis/client";

const getDefaultValues = (initialData: ProjectUpdateDTO): ProjectEditFormModel => {
    return {
        projectId: initialData.projectId || "",
        projectName: initialData.projectName || "",
        description: initialData.description || "",
    };
};

const useInitProjectEditForm = (initialData: ProjectUpdateDTO) => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues(initialData);

    const schema = yup.object().shape({
        projectId: yup.string()
        .default(defaultValues.projectId),
        projectName: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.projectName",
                    }),
                }))
            .default(defaultValues.projectName),
        description: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.description",
                    }),
                }))
            .default(defaultValues.description),
    });

    const resolver = yupResolver(schema);

    return { defaultValues, resolver };
};

export const useProjectEditFormController = (
    initialData: ProjectUpdateDTO,
    onSubmit?: () => void
): ProjectEditFormController => {
    const { defaultValues, resolver } = useInitProjectEditForm(initialData);
    const { editProject: { mutation, key: mutationKey }, getProjects: { key: queryKey } } = useProjectApi();
    const { mutateAsync: edit, status } = useMutation({
        mutationKey: [mutationKey], 
        mutationFn: mutation
    });
    const queryClient = useQueryClient();

    const submit = useCallback((data: ProjectEditFormModel) => {
        console.log('Data before sending to backend:', data);  // Print data before sending
        edit(data).then(() => {
            queryClient.invalidateQueries({ queryKey: [queryKey] });
            console.log(data);

            if (onSubmit) {
                onSubmit();
            }
        }).catch(error => {
            console.error('Submission error:', error);
        });
    }, [edit, queryClient, onSubmit]);

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        formState: { errors }
    } = useForm<ProjectEditFormModel>({
        defaultValues,
        resolver
    });

    return {
        actions: {
            handleSubmit,
            submit,
            register,
            watch,
        },
        computed: {
            defaultValues,
            isSubmitting: status === "pending"
        },
        state: {
            errors
        }
    }
}