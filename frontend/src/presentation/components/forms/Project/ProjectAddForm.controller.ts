import { ProjectAddFormController, ProjectAddFormModel } from "./ProjectAddForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useProjectApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: ProjectAddFormModel) => {
    const defaultValues = {
        projectName: '',
        description: '',
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
const useInitProjectAddForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({
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
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useProjectAddFormController = (onSubmit?: () => void): ProjectAddFormController => {
    const { defaultValues, resolver } = useInitProjectAddForm();
    const { addProject: { mutation, key: mutationKey }, getProjects: { key: queryKey } } = useProjectApi();
    const { mutateAsync: add, status } = useMutation({
        mutationKey: [mutationKey], 
        mutationFn: mutation
    });
    const queryClient = useQueryClient();

    const submit = useCallback((data: ProjectAddFormModel) => {
        console.log('Form Data:', data); // Log form data here
        add(data).then(() => {
            queryClient.invalidateQueries({ queryKey: [queryKey] }); // Adjust the query key as needed

            if (onSubmit) {
                onSubmit();
            }
        }).catch(error => {
            console.error('Submission error:', data); // Handle any errors
        });
        }, [add, queryClient, onSubmit]);

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        formState: { errors }
    } = useForm<ProjectAddFormModel>({ // Use the useForm hook to get callbacks and variables to work with the form.
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
            isSubmitting: status === "pending" // Return if the form is still submitting or nit.
        },
        state: {
            errors // Return what errors have occurred when validating the form input.
        }
    }
}