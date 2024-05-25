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
import { useProjectEditFormController } from "./ProjectEditForm.controller";
import { isEmpty, isUndefined } from "lodash";
import { ProjectEditFormModel } from "./ProjectEditForm.types";
import { ProjectUpdateDTO } from "@infrastructure/apis/client";

interface ProjectEditFormProps {
    initialData: ProjectUpdateDTO;
    onSubmit?: () => void;
}

export const ProjectEditForm: React.FC<ProjectEditFormProps> = ({ initialData, onSubmit }) => {
    const { formatMessage } = useIntl();
    const { state, actions, computed } = useProjectEditFormController(initialData, onSubmit); 

    const handleSubmit: (data: ProjectEditFormModel) => void = (data) => {
		console.log("aici");
        actions.submit(data);
    };

    return (
        <form onSubmit={actions.handleSubmit(handleSubmit)}>
            <Stack spacing={4} style={{ width: "500px" }}>
                <div>
                    <Grid container item direction="column" xs={12} rowSpacing={4}>
                        <Grid container item direction="column" xs={6} md={6}>
                            <FormControl fullWidth error={!isUndefined(state.errors.projectName)}>
                                <FormLabel>
                                    <FormattedMessage id="globals.projectName" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("projectName")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        {
                                            fieldName: formatMessage({
                                                id: "globals.projectName",
                                            }),
                                        }
                                    )}
                                    autoComplete="none"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.projectName)}>
                                    {state.errors.projectName?.message}
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
