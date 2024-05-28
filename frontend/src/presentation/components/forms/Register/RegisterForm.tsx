import {
    Button,
    CircularProgress,
    FormControl,
    FormHelperText,
    FormLabel,
    Grid,
    Stack,
    OutlinedInput,
    Select,
    MenuItem
} from "@mui/material";
import { FormattedMessage, useIntl } from "react-intl";
import { useRegisterFormController } from "./RegisterForm.controller";
import { ContentCard } from "@presentation/components/ui/ContentCard";
import { isEmpty, isUndefined } from "lodash";
import { UserRoleEnum } from "@infrastructure/apis/client";

/**
 * Here we declare the user add form component.
 * This form may be used in modals so the onSubmit callback could close the modal on completion.
 */
export const RegisterForm = (props: { onSubmit?: () => void }) => {
    const { formatMessage } = useIntl();
    const { state, actions, computed } = useRegisterFormController(props.onSubmit); // Use the controller.

    return (
        <form onSubmit={actions.handleSubmit(actions.submit)}>
            <Stack spacing={4} style={{ width: "700px" }}>
                <ContentCard title={formatMessage({ id: "globals.register" })}>
                    <Grid container item direction="row" xs={12} columnSpacing={4}>
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl 
                                fullWidth
                                error={!isUndefined(state.errors.name)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.name" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("name")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        { fieldName: formatMessage({ id: "globals.name" }) }
                                    )}
                                    autoComplete="name"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.name)}>
                                    {state.errors.name?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl
                                fullWidth
                                error={!isUndefined(state.errors.email)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.email" />
                                </FormLabel>
                                <OutlinedInput
                                    {...actions.register("email")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        { fieldName: formatMessage({ id: "globals.email" }) }
                                    )}
                                    autoComplete="username"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.email)}>
                                    {state.errors.email?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl
                                fullWidth
                                error={!isUndefined(state.errors.role)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.role" />
                                </FormLabel>
                                <Select
                                    {...actions.register("role")}
                                    value={actions.watch("role")}
                                    onChange={actions.selectRole}
                                    displayEmpty
                                >
                                    <MenuItem value="" disabled>
                                        <span className="text-gray">
                                            {formatMessage({ id: "globals.placeholders.selectInput" }, {
                                                fieldName: formatMessage({ id: "globals.role" })
                                            })}
                                        </span>
                                    </MenuItem>
                                    <MenuItem value={UserRoleEnum.Admin}>
                                        <FormattedMessage id="globals.admin" />
                                    </MenuItem>
                                    <MenuItem value={UserRoleEnum.Member}>
                                        <FormattedMessage id="globals.member" />
                                    </MenuItem>
                                </Select>
                                <FormHelperText hidden={isUndefined(state.errors.role)}>
                                    {state.errors.role?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl
                                fullWidth
                                error={!isUndefined(state.errors.password)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.password" />
                                </FormLabel>
                                <OutlinedInput
                                    type="password"
                                    {...actions.register("password")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        { fieldName: formatMessage({ id: "globals.password" }) }
                                    )}
                                    autoComplete="current-password"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.password)}>
                                    {state.errors.password?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                        <Grid container item direction="column" xs={12} md={12} style={{ marginBottom: "1rem" }}>
                            <FormControl
                                fullWidth
                                error={!isUndefined(state.errors.confirmPassword)}
                            >
                                <FormLabel required>
                                    <FormattedMessage id="globals.confirmPassword" />
                                </FormLabel>
                                <OutlinedInput
                                    type="password"
                                    {...actions.register("confirmPassword")}
                                    placeholder={formatMessage(
                                        { id: "globals.placeholders.textInput" },
                                        { fieldName: formatMessage({ id: "globals.confirmPassword" }) }
                                    )}
                                    autoComplete="current-password"
                                />
                                <FormHelperText hidden={isUndefined(state.errors.confirmPassword)}>
                                    {state.errors.confirmPassword?.message}
                                </FormHelperText>
                            </FormControl>
                        </Grid>
                    </Grid>
                </ContentCard>
                <Grid container item direction="row" xs={12} className="padding-top-sm">
                    <Grid container item direction="column" xs={12} md={7}></Grid>
                    <Grid container item direction="column" xs={5}>
                        <Button type="submit" disabled={!isEmpty(state.errors) || computed.isSubmitting}>
                            {!computed.isSubmitting && <FormattedMessage id="globals.submit" />}
                            {computed.isSubmitting && <CircularProgress />}
                        </Button>
                    </Grid>
                </Grid>
            </Stack>
        </form>
    );
};
