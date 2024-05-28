import { RegisterFormController, RegisterFormModel } from "./RegisterForm.types";
import { yupResolver } from "@hookform/resolvers/yup";
import { useIntl } from "react-intl";
import * as yup from "yup";
import { isUndefined } from "lodash";
import { useForm } from "react-hook-form";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useUserApi, useLoginApi } from "@infrastructure/apis/api-management";
import { useCallback } from "react";
import { UserRoleEnum } from "@infrastructure/apis/client";
import { SelectChangeEvent } from "@mui/material";
import { useAppRouter } from "@infrastructure/hooks/useAppRouter";
import { useAppDispatch } from "@application/store";
import { setToken } from "@application/state-slices";
import { toast } from "react-toastify";

/**
 * Use a function to return the default values of the form and the validation schema.
 * You can add other values as the default, for example when populating the form with data to update an entity in the backend.
 */
const getDefaultValues = (initialData?: RegisterFormModel) => {
    const defaultValues = {
        email: "",
        name: "",
        password: "",
        confirmPassword: "", // Add confirmPassword default value
        role: "" as UserRoleEnum
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
const useInitRegisterForm = () => {
    const { formatMessage } = useIntl();
    const defaultValues = getDefaultValues();

    const schema = yup.object().shape({
        name: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.name",
                    }),
                }))
            .default(defaultValues.name),
        email: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.email",
                    }),
                }))
            .email()
            .default(defaultValues.email),
        password: yup.string()
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.password",
                    }),
                })),
        confirmPassword: yup.string()
            .oneOf([yup.ref('password'), undefined], formatMessage(
                { id: "globals.validations.passwordsMustMatch" }
            ))
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.confirmPassword",
                    }),
                })),
        role: yup.string()
            .oneOf([ // The select input should have one of these values.
                UserRoleEnum.Admin,
                UserRoleEnum.Member,
                // UserRoleEnum.Client
            ])
            .required(formatMessage(
                { id: "globals.validations.requiredField" },
                {
                    fieldName: formatMessage({
                        id: "globals.role",
                    }),
                }))
            .default(defaultValues.role)
    });

    const resolver = yupResolver(schema);

    return { defaultValues, resolver };
}

/**
 * Create a controller hook for the form and return any data that is necessary for the form.
 */
export const useRegisterFormController = (onSubmit?: () => void): RegisterFormController => {
    const { defaultValues, resolver } = useInitRegisterForm();
    const { addUser: { mutation, key: mutationKey }, getUsers: { key: queryKey } } = useUserApi();
    const { loginMutation: { mutation: loginMutation, key: loginMutationKey } } = useLoginApi();
    const { mutateAsync: add, status: addStatus } = useMutation({
        mutationKey: [mutationKey], 
        mutationFn: mutation
    });
    const { mutateAsync: login, status: loginStatus } = useMutation({
        mutationKey: [loginMutationKey],
        mutationFn: loginMutation
    });
    const queryClient = useQueryClient();
    const { redirectToHome } = useAppRouter();
    const dispatch = useAppDispatch();
    const { formatMessage } = useIntl();

    const submit = useCallback(async (data: RegisterFormModel) => {
        try {
            await add(data);
            await queryClient.invalidateQueries({ queryKey: [queryKey] });
            const loginResult = await login({ email: data.email, password: data.password });
            dispatch(setToken(loginResult.response?.token ?? ''));
            toast(formatMessage({ id: "notifications.messages.authenticationSuccess" }));
            redirectToHome();

            if (onSubmit) {
                onSubmit();
            }
        } catch (error) {
            console.error(error);
        }
    }, [add, login, queryClient, queryKey, dispatch, formatMessage, redirectToHome, onSubmit]);

    const {
        register,
        handleSubmit,
        watch,
        setValue,
        formState: { errors }
    } = useForm<RegisterFormModel>({ 
        defaultValues, 
        resolver 
    });

    const selectRole = useCallback((event: SelectChangeEvent<UserRoleEnum>) => { 
        setValue("role", event.target.value as UserRoleEnum, {
            shouldValidate: true,
        });
    }, [setValue]);

    return {
        actions: { 
            handleSubmit, 
            submit, 
            register, 
            watch, 
            selectRole
        },
        computed: {
            defaultValues,
            isSubmitting: addStatus === "pending" || loginStatus === "pending" 
        },
        state: {
            errors 
        }
    }
}
