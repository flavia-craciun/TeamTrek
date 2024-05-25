import { QuestionEditFormController, QuestionEditFormModel } from "./QuestionEditForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useQuestionApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";
import { QuestionUpdateDTO } from "@infrastructure/apis/client";

const getDefaultValues = (initialData: QuestionUpdateDTO): QuestionEditFormModel => {
    return {
        questionId: initialData.questionId || "",
        title: initialData.title || "",
        description: initialData.description || "",
    };
};

const useInitQuestionEditForm = (initialData: QuestionUpdateDTO) => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues(initialData);

    const schema = yup.object().shape({
        questionId: yup.string()
        .default(defaultValues.questionId),
        title: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.questionTitle",
                    }),
                }))
            .default(defaultValues.title),
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

export const useQuestionEditFormController = (
    initialData: QuestionUpdateDTO,
    onSubmit?: () => void
): QuestionEditFormController => {
    const { defaultValues, resolver } = useInitQuestionEditForm(initialData);
    const { editQuestion: { mutation, key: mutationKey }, getQuestions: { key: queryKey } } = useQuestionApi();
    const { mutateAsync: edit, status } = useMutation({
        mutationKey: [mutationKey], 
        mutationFn: mutation
    });
    const queryClient = useQueryClient();

    const submit = useCallback((data: QuestionEditFormModel) => {
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
    } = useForm<QuestionEditFormModel>({
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