import { useAppSelector } from "@application/store";
import { ApiQuestionGetQuestionsGetRequest, QuestionAddDTO, QuestionApi, QuestionUpdateDTO } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const getQuestionsQueryKey = "getQuestionsQuery";
// const getQuestionQueryKey = "getQuestionQuery";
const addQuestionMutationKey = "addQuestionMutation";
const editQuestionMutationKey = "editQuestionMutation";
const deleteQuestionMutationKey = "deleteQuestionMutation";
const getQuestionAnswersMutationKey = "getQuestionAnswers"

/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the question API.
 */
export const useQuestionApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const getQuestions = (page: ApiQuestionGetQuestionsGetRequest) => new QuestionApi(config).apiQuestionGetQuestionsGet(page); // Use the generated client code and adapt it.
    // const getQuestion = (id: string) => new QuestionApi(config).apiQuestionGetQuestionQuestionIdGetRaw({id});
    const addQuestion = (question: QuestionAddDTO) => new QuestionApi(config).apiQuestionAddPost({ questionAddDTO: question });
    const editQuestion = (question: QuestionUpdateDTO) => new QuestionApi(config).apiQuestionUpdatePut({ questionUpdateDTO: question });
    const deleteQuestion = (questionId: string) => new QuestionApi(config).apiQuestionDeleteQuestionIdDelete({ questionId });
    const getQuestionAnswers = (questionId: string) => new QuestionApi(config).apiQuestionGetQuestionAnswersQuestionIdGet({ questionId });

    return {
        getQuestions: { // Return the query object.
            key: getQuestionsQueryKey, // Add the key to identify the query.
            query: getQuestions // Add the query callback.
        },
        // getQuestion: {
        //     key: getQuestionQueryKey,
        //     query: getQuestion
        // },
        addQuestion: { // Return the mutation object.
            key: addQuestionMutationKey, // Add the key to identify the mutation.
            mutation: addQuestion // Add the mutation callback.
        },
        editQuestion: { // Return the mutation object.
            key: editQuestionMutationKey, // Add the key to identify the mutation.
            mutation: editQuestion // Add the mutation callback.
        },
        deleteQuestion: {
            key: deleteQuestionMutationKey,
            mutation: deleteQuestion
        },
        getQuestionAnswers: {
            key: getQuestionAnswersMutationKey,
            mutation: getQuestionAnswers
        }
    }
}