import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";

export type ProjectEditFormModel = {
    projectId: string;
    projectName: string;
    description: string;
};

export type ProjectEditFormState = {
    errors: FieldErrorsImpl<DeepRequired<ProjectEditFormModel>>;
};

export type ProjectEditFormActions = {
    register: UseFormRegister<ProjectEditFormModel>;
    watch: UseFormWatch<ProjectEditFormModel>;
    handleSubmit: UseFormHandleSubmit<ProjectEditFormModel>;
    submit: (body: ProjectEditFormModel) => void;
};
export type ProjectEditFormComputed = {
    defaultValues: ProjectEditFormModel,
    isSubmitting: boolean
};

export type ProjectEditFormController = FormController<ProjectEditFormState, ProjectEditFormActions, ProjectEditFormComputed>;