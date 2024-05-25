import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";

export type QuestionEditFormModel = {
    questionId: string;
    title: string;
    description: string;
};

export type QuestionEditFormState = {
    errors: FieldErrorsImpl<DeepRequired<QuestionEditFormModel>>;
};

export type QuestionEditFormActions = {
    register: UseFormRegister<QuestionEditFormModel>;
    watch: UseFormWatch<QuestionEditFormModel>;
    handleSubmit: UseFormHandleSubmit<QuestionEditFormModel>;
    submit: (body: QuestionEditFormModel) => void;
};
export type QuestionEditFormComputed = {
    defaultValues: QuestionEditFormModel,
    isSubmitting: boolean
};

export type QuestionEditFormController = FormController<QuestionEditFormState, QuestionEditFormActions, QuestionEditFormComputed>;