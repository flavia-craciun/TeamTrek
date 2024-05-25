import {
    Button,
    CircularProgress,
    FormControl,
    FormHelperText,
    FormLabel,
    Grid,
    Stack,
    OutlinedInput,
} from "@mui/material";
import { FormattedMessage, useIntl } from "react-intl";
import { useQuestionEditFormController } from "./QuestionEditForm.controller";
import { isEmpty, isUndefined } from "lodash";
import { QuestionEditFormModel } from "./QuestionEditForm.types";
import { QuestionUpdateDTO } from "@infrastructure/apis/client";

interface QuestionEditFormProps {
    initialData: QuestionUpdateDTO;
    onSubmit?: () => void;
}

export const QuestionEditForm: React.FC<QuestionEditFormProps> = ({ initialData, onSubmit }) => {
    const { formatMessage } = useIntl();
    const { state, actions, computed } = useQuestionEditFormController(initialData, onSubmit); 

    const handleSubmit: (data: QuestionEditFormModel) => void = (data) => {
		console.log("aici");
        actions.submit(data);
    };

    return (
        <form onSubmit={actions.handleSubmit(handleSubmit)}>
            <Stack spacing={4} style={{ width: "500px" }}>
                <div>
                    <Grid container item direction="column" xs={12} rowSpacing={4}>
                        <Grid container item direction="column" xs={6} md={6}>
                            <FormControl fullWidth error={!isUndefined(state.errors.title)}>
                                <FormLabel>
                                    <FormattedMessage id="globals.questionTitle" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("title")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        {
                                            fieldName: formatMessage({
                                                id: "globals.questionTitle",
                                            }),
                                        }
                                    )}
                                    autoComplete="none"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.title)}>
                                    {state.errors.title?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                        <Grid container item direction="column" xs={6} md={6}>
                            <FormControl fullWidth error={!isUndefined(state.errors.description)}>
                                <FormLabel>
                                    <FormattedMessage id="globals.description" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("description")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        {
                                            fieldName: formatMessage({
                                                id: "globals.description",
                                            }),
                                        }
                                    )}
                                    autoComplete="none"
                                    multiline
                                    minRows={10}
                                />
                                <FormHelperText hidden={isUndefined(state.errors.description)}>
                                    {state.errors.description?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                    </Grid>
                    <Grid container item direction="row" xs={12} className="pediting-top-sm">
                        <Grid container item direction="column" xs={12} md={7}></Grid>
                        <Grid container item direction="column" xs={5}>
                            <Button
                                type="submit"
                                disabled={!isEmpty(state.errors) || computed.isSubmitting}
                            >
                                {!computed.isSubmitting && <FormattedMessage id="globals.submit" />}
                                {computed.isSubmitting && <CircularProgress />}
                            </Button>
                        </Grid>
                    </Grid>
                </div>
            </Stack>
        </form>
    );
};
