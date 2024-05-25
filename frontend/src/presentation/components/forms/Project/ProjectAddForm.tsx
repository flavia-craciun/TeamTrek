import {
	Button,
	CircularProgress,
	FormControl,
	FormHelperText,
	FormLabel,
	Grid,
	Stack,
	OutlinedInput,
	TextareaAutosize,
	MenuItem,
} from "@mui/material";
import { FormattedMessage, useIntl } from "react-intl";
import { useProjectAddFormController } from "./ProjectAddForm.controller";
import { isEmpty, isUndefined } from "lodash";
import { ProjectAddFormModel } from "./ProjectAddForm.types";

/**
 * Here we declare the project add form component.
 * This form may be used in modals so the onSubmit callback could close the modal on completion.
 */
export const ProjectAddForm = (props: { onSubmit?: () => void }) => {
	const { formatMessage } = useIntl();
	const { state, actions, computed } = useProjectAddFormController(props.onSubmit); // Use the controller.

    const handleSubmit = (data: ProjectAddFormModel) => {
        console.log('Form Data:', data); // Log form data
        actions.submit(data);
    };

	return (
        <form onSubmit={actions.handleSubmit(handleSubmit)}>
			{" "}
			{/* Wrap your form into a form tag and use the handle submit callback to validate the form and call the data submission. */}
			<Stack spacing={4} style={{ width: "500px" }}>
				<div>
					<Grid container item direction="column" xs={12} rowSpacing={4}>
						<Grid container item direction="column" xs={6} md={6}>
							<FormControl fullWidth error={!isUndefined(state.errors.projectName)}>
								{" "}
								{/* Wrap the input into a form control and use the errors to show the input invalid if needed. */}
								<FormLabel required>
									<FormattedMessage id="globals.projectName" />
								</FormLabel>{" "}
								{/* Add a form label to indicate what the input means. */}
								<OutlinedInput
									{...actions.register("projectName")} // Bind the form variable to the UI input.
									placeholder={formatMessage(
										{ id: "globals.placeholders.textInput" },
										{
											fieldName: formatMessage({
												id: "globals.projectName",
											}),
										}
									)}
									autoComplete="none"
								/>{" "}
								{/* Add a input like a textbox shown here. */}
								<FormHelperText hidden={isUndefined(state.errors.projectName)}>
									{state.errors.projectName?.message}
								</FormHelperText>{" "}
								{/* Add a helper text that is shown then the input has a invalid value. */}
							</FormControl>
						</Grid>
						<Grid container item direction="column" xs={6} md={6}>
							<FormControl
								fullWidth
								error={!isUndefined(state.errors.description)}
							>
								<FormLabel required>
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
					<Grid
						container
						item
						direction="row"
						xs={12}
						className="padding-top-sm"
					>
						<Grid container item direction="column" xs={12} md={7}></Grid>
						<Grid container item direction="column" xs={5}>
							<Button
								type="submit"
								disabled={!isEmpty(state.errors) || computed.isSubmitting}
							>
								{" "}
								{/* Add a button with type submit to call the submission callback if the button is a descended of the form element. */}
								{!computed.isSubmitting && (
									<FormattedMessage id="globals.submit" />
								)}
								{computed.isSubmitting && <CircularProgress />}
							</Button>
						</Grid>
					</Grid>
				</div>
			</Stack>
		</form>
	);
};
