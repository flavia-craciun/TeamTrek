import { Button, Dialog, DialogContent, DialogTitle } from "@mui/material";
import { useQuestionAddDialogController } from "./QuestionAddDialog.controller";
import { QuestionAddForm } from "@presentation/components/forms/Question/QuestionAddForm";
import { useIntl } from "react-intl";

/**
 * This component wraps the question add form into a modal dialog.
 */
export const QuestionAddDialog = () => {
  const { open, close, isOpen } = useQuestionAddDialogController();
  const { formatMessage } = useIntl();

  return <div>
    <Button variant="outlined" onClick={open}>
      {formatMessage({ id: "labels.addQuestion" })}
    </Button>
    <Dialog
      open={isOpen}
      onClose={close}>
      <DialogTitle>
        {formatMessage({ id: "labels.addQuestion" })}
      </DialogTitle>
      <DialogContent>
        <QuestionAddForm onSubmit={close} />
      </DialogContent>
    </Dialog>
  </div>
};