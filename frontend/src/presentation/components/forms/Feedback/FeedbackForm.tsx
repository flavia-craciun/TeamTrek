import React from 'react';
import {
    Button,
    CircularProgress,
    FormControl,
    FormHelperText,
    FormLabel,
    Grid,
    Stack,
    MenuItem,
    Select,
    Radio,
    RadioGroup,
    FormControlLabel,
    Checkbox,
    TextField
} from "@mui/material";
import { FormattedMessage, useIntl } from "react-intl";
import { useFeedbackFormController } from "./FeedbackForm.controller";
import { isEmpty, isUndefined } from "lodash";

export const FeedbackForm = () => {
    const { formatMessage } = useIntl();
    const { state, actions, computed } = useFeedbackFormController(); // Use the controller.

    return (
        <form onSubmit={actions.handleSubmit(actions.submit)}> {/* Wrap your form into a form tag and use the handle submit callback to validate the form and call the data submission. */}
            <Stack spacing={4} style={{ width: "95%" }}>
                    <Grid container item direction="row" xs={12} columnSpacing={4}>
                        {/* Radio buttons */}
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl fullWidth error={!isUndefined(state.errors.rating)}>
                                <FormLabel required>
                                    <FormattedMessage id="globals.rating" />
                                </FormLabel>
                                <RadioGroup {...actions.register("rating")} row>
                                    <FormControlLabel value="1" control={<Radio />} label="1" />
                                    <FormControlLabel value="2" control={<Radio />} label="2" />
                                    <FormControlLabel value="3" control={<Radio />} label="3" />
                                    <FormControlLabel value="4" control={<Radio />} label="4" />
                                    <FormControlLabel value="5" control={<Radio />} label="5" />
                                </RadioGroup>
                                <FormHelperText hidden={isUndefined(state.errors.rating)}>
                                    {state.errors.rating?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>

                        {/* Select dropdown */}
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl fullWidth error={!isUndefined(state.errors.category)}>
                                <FormLabel required>
                                    <FormattedMessage id="globals.category" />
                                </FormLabel>
                                <Select
                                    {...actions.register("category")}
                                    displayEmpty
                                    defaultValue=""
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.selectInput" },
                                        { fieldName: formatMessage({ id: "globals.category" }) }
                                    )}
                                >
                                    <MenuItem value="">
                                        <em>{formatMessage({ id: "globals.placeholders.select"})}</em>
                                    </MenuItem>
                                    <MenuItem value="feature">{formatMessage({ id: "globals.users" })}</MenuItem>
                                    <MenuItem value="bug">{formatMessage({ id: "globals.projects" })}</MenuItem>
                                    <MenuItem value="other">{formatMessage({ id: "globals.questions" })}</MenuItem>
                                </Select>
                                <FormHelperText hidden={isUndefined(state.errors.category)}>
                                    {state.errors.category?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>

                        {/* Text box */}
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl fullWidth error={!isUndefined(state.errors.comments)}>
                                <FormLabel>
                                    <FormattedMessage id="globals.comments" />
                                </FormLabel>
                                <TextField
                                    {...actions.register("comments")}
                                    multiline
                                    rows={4}
                                />
                                <FormHelperText hidden={isUndefined(state.errors.comments)}>
                                    {state.errors.comments?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>

                        {/* Checkbox */}
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl fullWidth error={!isUndefined(state.errors.agree)}>
                                <FormControlLabel
                                    control={
                                        <Checkbox
                                            {...actions.register("agree")}
                                            color="primary"
                                        />
                                    }
                                    label={formatMessage({ id: "globals.agreeTerms" })}
                                />
                                <FormHelperText hidden={isUndefined(state.errors.agree)}>
                                    {state.errors.agree?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                    </Grid>
                <Grid container item direction="column" xl={10}>
                        <Button type="submit" disabled={!isEmpty(state.errors) || computed.isSubmitting} style={{marginLeft: 50}}>
                            {!computed.isSubmitting && <FormattedMessage id="labels.submit" />}
                            {computed.isSubmitting && <CircularProgress />}
                        </Button>
                    </Grid>
            </Stack>
        </form>
    );
};
