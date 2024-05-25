import React, { useEffect } from "react";
import { Dialog, DialogContent, DialogTitle, IconButton } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { useIntl } from "react-intl";
import { ProjectUpdateDTO } from "@infrastructure/apis/client";
import { ProjectEditForm } from "@presentation/components/forms/Project/ProjectEditForm";

interface ProjectEditDialogProps {
    initialData: ProjectUpdateDTO;
    isOpen: boolean;
    onClose: () => void;
}

export const ProjectEditDialog: React.FC<ProjectEditDialogProps> = ({ initialData, isOpen, onClose }) => {
    const { formatMessage } = useIntl();

    useEffect(() => {
        console.log('Initial Data:', initialData);
    }, [initialData]);

    return (
        <Dialog open={isOpen} onClose={onClose}>
            <DialogTitle>
                {formatMessage({ id: "labels.editProject" })}
            </DialogTitle>
            <DialogContent>
                <ProjectEditForm initialData={initialData} onSubmit={onClose} />
            </DialogContent>
        </Dialog>
    );
};


