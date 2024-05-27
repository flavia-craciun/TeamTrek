import { useAppSelector } from "@application/store";
import { FeedbackAddDTO, FeedbackApi, FeedbackUpdateDTO } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

// Define constants for mutation and query keys
const getFeedbackQueryKey = "getFeedbackQuery";
const addFeedbackMutationKey = "addFeedbackMutation";
const editFeedbackMutationKey = "editFeedbackMutation";
const deleteFeedbackMutationKey = "deleteFeedbackMutation";

// Returns an object with callbacks for managing the feedback API
export const useFeedbackApi = () => {
    const { token } = useAppSelector(x => x.profileReducer);
    const config = getAuthenticationConfiguration(token);

    const getFeedback = (feedbackId: string) => new FeedbackApi(config).apiFeedbackGetFeedbackFeedbackIdGet({ feedbackId });
    const addFeedback = (feedback: FeedbackAddDTO) => new FeedbackApi(config).apiFeedbackAddPost({ feedbackAddDTO: feedback });
    const editFeedback = (feedback: FeedbackUpdateDTO) => new FeedbackApi(config).apiFeedbackUpdatePut({ feedbackUpdateDTO: feedback });
    const deleteFeedback = (feedbackId: string) => new FeedbackApi(config).apiFeedbackDeleteFeedbackIdDelete({ feedbackId });

    return {
        getFeedback: {
            key: getFeedbackQueryKey,
            query: getFeedback
        },
        addFeedback: {
            key: addFeedbackMutationKey,
            mutation: addFeedback
        },
        editFeedback: {
            key: editFeedbackMutationKey,
            mutation: editFeedback
        },
        deleteFeedback: {
            key: deleteFeedbackMutationKey,
            mutation: deleteFeedback
        }
    }
}
