import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";

export type QuestionAddFormModel = {
    title: string;
    description: string;
};

export type QuestionAddFormState = {
    errors: FieldErrorsImpl<DeepRequired<QuestionAddFormModel>>;
};

export type QuestionAddFormActions = {
    register: UseFormRegister<QuestionAddFormModel>;
    watch: UseFormWatch<QuestionAddFormModel>;
    handleSubmit: UseFormHandleSubmit<QuestionAddFormModel>;
    submit: (body: QuestionAddFormModel) => void;
};
export type QuestionAddFormComputed = {
    defaultValues: QuestionAddFormModel,
    isSubmitting: boolean
};

export type QuestionAddFormController = FormController<QuestionAddFormState, QuestionAddFormActions, QuestionAddFormComputed>;