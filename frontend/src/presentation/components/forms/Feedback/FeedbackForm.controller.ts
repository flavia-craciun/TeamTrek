import { FeedbackFormController, FeedbackFormModel } from "./FeedbackForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useCallback } from "react";
import { toast } from "react-toastify";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: { category: string, rating: string, agree: boolean, comments: string }) => {
    const defaultValues = {
        category: "",
        rating: "",
        agree: false,
        comments: ""
    };

    if (!isUndefined(initialData)) {
        return {
            ...defaultValues,
            ...initialData,
        };
    }

    return defaultValues;
};

/**
 * Create a hook to get the validation schema.
 */
const useInitFeedbackForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({ // Use yup to build the validation schema of the form.
        category: yup.string() // This field should be a string.
            .required(formatMessage( // Use formatMessage to get the translated error message.
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({ // Format the message with other translated strings.
                        id: "globals.category",
                    }),
                })) // The field is required and needs an error message when it is empty.
            .default(defaultValues.category), // Add a default value for the field.
        rating: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.rating",
                    }),
                }))
            .default(defaultValues.rating),
        agree: yup.boolean()
            .oneOf([true], formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.agreeTerms",
                    }),
                }))
            .default(defaultValues.agree),
        comments: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.comments",
                    }),
                }))
            .default(defaultValues.comments),
    });

    const resolver = yupResolver(schema); // Get the resolver.

    return { defaultValues, resolver }; // Return the default values and the resolver.
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useFeedbackFormController = (): FeedbackFormController => {
    const { formatMessage } = useIntl();
    const { defaultValues, resolver } = useInitFeedbackForm();

    const submit = useCallback((data: FeedbackFormModel) => { // Create a submit callback to log the form data.
        console.log("Submitted feedback:", data);
        toast(formatMessage({ id: "notifications.messages.feedbackSuccess" }));
    }, [formatMessage]);

    const {
        register,
        handleSubmit,
        watch,
        formState: { errors }
    } = useForm<FeedbackFormModel>({ // Use the useForm hook to get callbacks and variables to work with the form.
        defaultValues, // Initialize the form with the default values.
        resolver // Add the validation resolver.
    });

    return {
        actions: { // Return any callbacks needed to interact with the form.
            handleSubmit, // Add the form submit handle.
            submit, // Add the submit handle that needs to be passed to the submit handle.
            register, // Add the variable register to bind the form fields in the UI with the form variables.
            watch, // Add a watch on the variables, this function can be used to watch changes on variables if it is needed in some locations.
        },
        computed: {
            defaultValues,
            isSubmitting: false // Return if the form is still submitting or not. As we don't have async submission, this is always false.
        },
        state: {
            errors // Return what errors have occurred when validating the form input.
        }
    }
}
