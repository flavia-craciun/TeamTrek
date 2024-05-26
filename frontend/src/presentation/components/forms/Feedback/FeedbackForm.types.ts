import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";

export type FeedbackFormModel = {
    category: string;
    rating: string;
    agree: boolean;
    comments: string;
};

export type FeedbackFormState = {
    errors: FieldErrorsImpl<DeepRequired<FeedbackFormModel>>;
};

export type FeedbackFormActions = {
    register: UseFormRegister<FeedbackFormModel>;
    watch: UseFormWatch<FeedbackFormModel>;
    handleSubmit: UseFormHandleSubmit<FeedbackFormModel>;
    submit: (body: FeedbackFormModel) => void;
};
export type FeedbackFormComputed = {
    defaultValues: FeedbackFormModel,
    isSubmitting: boolean
};

export type FeedbackFormController = FormController<FeedbackFormState, FeedbackFormActions, FeedbackFormComputed>;