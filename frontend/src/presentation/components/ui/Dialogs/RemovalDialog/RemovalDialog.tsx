import React from "react";
import { Dialog, DialogContent, DialogTitle, Typography, Button, Grid, Box } from "@mui/material"; // Import Box component
import { useIntl } from "react-intl";

interface RemovalDialogProps {
    objectName: string;
    isOpen: boolean;
    onClose: () => void;
    onConfirm: () => void;
}

export const RemovalDialog: React.FC<RemovalDialogProps> = ({ objectName, isOpen, onClose, onConfirm }) => {
    const { formatMessage } = useIntl();

    return (
        <Dialog open={isOpen} onClose={onClose} maxWidth="sm" fullWidth>
            <DialogTitle>
                {formatMessage({ id: "labels.removalConfirmation" })}
            </DialogTitle>
            <DialogContent>
                <Box mb={2}>
                    <Typography variant="body1">
                        {formatMessage({ id: "globals.placeholders.removalText" }, { object: objectName })}
                    </Typography>
                </Box>
                <Grid container justifyContent="flex-end">
                    <Button onClick={onClose} color="primary" sx={{ mr: 1 }}>
                        {formatMessage({ id: "labels.cancel" })}
                    </Button>
                    <Button onClick={onConfirm} color="error">
                        {formatMessage({ id: "labels.confirm" })}
                    </Button>
                </Grid>
            </DialogContent>
        </Dialog>
    );
};
