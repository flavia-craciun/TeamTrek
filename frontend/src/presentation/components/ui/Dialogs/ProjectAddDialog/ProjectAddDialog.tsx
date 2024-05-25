import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import { useProjectAddDialogController } from "./ProjectAddDialog.controller";
import { ProjectAddForm } from "@presentation/components/forms/Project/ProjectAddForm";
import { useIntl } from "react-intl";

/**
 * This component wraps the project add form into a modal dialog.
 */
export const ProjectAddDialog = () => {
  const { open, close, isOpen } = useProjectAddDialogController();
  const { formatMessage } = useIntl();

  return <div>
    <Button variant="outlined" onClick={open}>
      {formatMessage({ id: "labels.addProject" })}
    </Button>
    <Dialog
      open={isOpen}
      onClose={close}>
      <DialogTitle>
        {formatMessage({ id: "labels.addProject" })}
      </DialogTitle>
      <DialogContent>
        <ProjectAddForm onSubmit={close} />
      </DialogContent>
    </Dialog>
  </div>
};