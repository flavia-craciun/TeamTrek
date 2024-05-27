import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useCallback } from "react";
import { toast } from "react-toastify";
import { FeedbackFormController, FeedbackFormModel } from "./FeedbackForm.types";
import { useFeedbackApi } from "@infrastructure/apis/api-management/feedback";

const getDefaultValues = (initialData?: FeedbackFormModel) => {
    const defaultValues = {
        suggestion: "",
        frequentedSection: "",
        responseWanted: false,
        rating: 0
    };

    if (initialData) {
        return {
            ...defaultValues,
            ...initialData,
        };
    }

    return defaultValues;
};

const useInitFeedbackForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({
        frequentedSection: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                { fieldName: formatMessage({ id: "globals.category" }) }
            ))
            .default(defaultValues.frequentedSection),
        rating: yup.number()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                { fieldName: formatMessage({ id: "globals.rating" }) }
            ))
            .default(defaultValues.rating),
        responseWanted: yup.boolean()
            .default(defaultValues.responseWanted),
        suggestion: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                { fieldName: formatMessage({ id: "globals.comments" }) }
            ))
            .default(defaultValues.suggestion),
    });

    const resolver = yupResolver(schema);

    return { defaultValues, resolver };
}

export const useFeedbackFormController = (onSubmit?: () => void): FeedbackFormController => {
    const { defaultValues, resolver } = useInitFeedbackForm();
    const { addFeedback: { mutation, key: mutationKey } } = useFeedbackApi();
    const queryClient = useQueryClient();
    const { mutateAsync: addFeedback, status } = useMutation({
        mutationKey: [mutationKey],
        mutationFn: mutation
    });

    const submit = useCallback(async (data: FeedbackFormModel) => {
        console.log('Form Data:', data);
        try {
            const formattedData = { 
                ...data, 
                rating: Number(data.rating),
                responseWanted: data.responseWanted ? 1 : 0
            };
            await addFeedback(formattedData);
            queryClient.invalidateQueries({ queryKey: [mutationKey] });

            toast.success("Feedback submitted successfully!");
            if (onSubmit) {
                onSubmit();
            }
        } catch (error) {
            console.error('Submission error:', error);
            toast.error("Failed to submit feedback.");
        }
    }, [addFeedback, queryClient, onSubmit, mutationKey]);

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        formState: { errors }
    } = useForm<FeedbackFormModel>({
        defaultValues,
        resolver
    });

    return {
        actions: {
            handleSubmit,
            submit,
            register,
            watch,
            setValue
        },
        computed: {
            defaultValues,
            isSubmitting: status === "pending"
        },
        state: {
            errors,
        }
    }
}
