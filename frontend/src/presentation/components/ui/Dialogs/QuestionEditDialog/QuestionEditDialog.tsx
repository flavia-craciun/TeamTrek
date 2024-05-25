import React, { useEffect } from "react";
import { Dialog, DialogContent, DialogTitle, IconButton } from "@mui/material";
import { useIntl } from "react-intl";
import { QuestionUpdateDTO } from "@infrastructure/apis/client";
import { QuestionEditForm } from "@presentation/components/forms/Question/QuestionEditForm";

interface QuestionEditDialogProps {
    initialData: QuestionUpdateDTO;
    isOpen: boolean;
    onClose: () => void;
}

export const QuestionEditDialog: React.FC<QuestionEditDialogProps> = ({ initialData, isOpen, onClose }) => {
    const { formatMessage } = useIntl();

    useEffect(() => {
        console.log('Initial Data:', initialData);
    }, [initialData]);

    return (
        <Dialog open={isOpen} onClose={onClose}>
            <DialogTitle>
                {formatMessage({ id: "labels.editQuestion" })}
            </DialogTitle>
            <DialogContent>
                <QuestionEditForm initialData={initialData} onSubmit={onClose} />
            </DialogContent>
        </Dialog>
    );
};


