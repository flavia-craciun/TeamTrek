import { FormController } from "../FormController";
import {
    UseFormHandleSubmit,
    UseFormRegister,
    FieldErrorsImpl,
    DeepRequired,
    UseFormWatch
} from "react-hook-form";

type SetValueFunction = (name: keyof FeedbackFormModel, value: any, options?: Record<string, any>) => void;

export type FeedbackFormModel = {
    frequentedSection: string;
    suggestion: string;
    responseWanted: boolean;
    rating: number;
};

export type FeedbackFormState = {
    errors: FieldErrorsImpl<DeepRequired<FeedbackFormModel>>;
};

export type FeedbackFormActions = {
    register: UseFormRegister<FeedbackFormModel>;
    watch: UseFormWatch<FeedbackFormModel>;
    handleSubmit: UseFormHandleSubmit<FeedbackFormModel>;
    submit: (body: FeedbackFormModel) => void;
    setValue: SetValueFunction; // Use the custom type here
};

export type FeedbackFormComputed = {
    defaultValues: FeedbackFormModel,
    isSubmitting: boolean
};

export type FeedbackFormController = FormController<FeedbackFormState, FeedbackFormActions, FeedbackFormComputed>;
