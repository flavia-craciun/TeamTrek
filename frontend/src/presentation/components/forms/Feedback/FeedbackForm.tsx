import React, { useState } from 'react';
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
    const { state, actions, computed } = useFeedbackFormController();
    const [rating, setRating] = useState(""); // State to hold the selected rating

    const handleRatingChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRating(event.target.value);
        actions.setValue("rating", event.target.value);
    };

    return (
        <form onSubmit={actions.handleSubmit(actions.submit)}>
            <Stack spacing={4} style={{ width: "95%" }}>
                <Grid container item direction="row" xs={12} columnSpacing={4}>
                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                        <FormControl fullWidth error={!isUndefined(state.errors.rating)}>
                            <FormLabel required>
                                <FormattedMessage id="globals.rating" />
                            </FormLabel>
                            <RadioGroup value={rating} onChange={handleRatingChange} row>
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

                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                        <FormControl fullWidth error={!isUndefined(state.errors.frequentedSection)}>
                            <FormLabel required>
                                <FormattedMessage id="globals.frequentedSection" />
                            </FormLabel>
                            <Select
                                {...actions.register("frequentedSection")}
                                displayEmpty
                                defaultValue=""
                                placeholder={formatMessage(
                                    { id: "globals.placeholders.selectInput" },
                                    { fieldName: formatMessage({ id: "globals.frequentedSection" }) }
                                )}
                            >
                                <MenuItem value="">
                                    <em>{formatMessage({ id: "globals.placeholders.select" })}</em>
                                </MenuItem>
                                <MenuItem value="Users">{formatMessage({ id: "globals.users" })}</MenuItem>
                                <MenuItem value="Projects">{formatMessage({ id: "globals.projects" })}</MenuItem>
                                <MenuItem value="Questions">{formatMessage({ id: "globals.questions" })}</MenuItem>
                            </Select>
                            <FormHelperText hidden={isUndefined(state.errors.frequentedSection)}>
                                {state.errors.frequentedSection?.message}
                            </FormHelperText>
                        </FormControl>
                    </Grid>

                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                        <FormControl fullWidth error={!isUndefined(state.errors.suggestion)}>
                            <FormLabel>
                                <FormattedMessage id="globals.suggestion" />
                            </FormLabel>
                            <TextField
                                {...actions.register("suggestion")}
                                multiline
                                rows={4}
                            />
                            <FormHelperText hidden={isUndefined(state.errors.suggestion)}>
                                {state.errors.suggestion?.message}
                            </FormHelperText>
                        </FormControl>
                    </Grid>

                    <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                        <FormControlLabel
                            control={<Checkbox {...actions.register("responseWanted")} />}
                            label={formatMessage({ id: "globals.responseWanted" })}
                        />
                    </Grid>
                </Grid>

                <Stack direction="row" spacing={4}>
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary"
                        disabled={computed.isSubmitting}
                    >
                        {computed.isSubmitting ? <CircularProgress size={24} /> : <FormattedMessage id="globals.submit" />}
                    </Button>
                </Stack>
            </Stack>
        </form>
    );
};
