import { useAppSelector } from "@application/store";
import { ApiProjectGetProjectsGetRequest, ProjectAddDTO, ProjectApi, ProjectUpdateDTO } from "../client";
import { getAuthenticationConfiguration } from "@infrastructure/utils/userUtils";

/**
 * Use constants to identify mutations and queries.
 */
const getProjectsQueryKey = "getProjectsQuery";
// const getProjectQueryKey = "getProjectQuery";
const addProjectMutationKey = "addProjectMutation";
const editProjectMutationKey = "editProjectMutation";
// const deleteProjectMutationKey = "deleteProjectMutation";

/**
 * Returns the an object with the callbacks that can be used for the React Query API, in this case to manage the project API.
 */
export const useProjectApi = () => {
    const { token } = useAppSelector(x => x.profileReducer); // You can use the data form the Redux storage. 
    const config = getAuthenticationConfiguration(token); // Use the token to configure the authentication header.

    const getProjects = (page: ApiProjectGetProjectsGetRequest) => new ProjectApi(config).apiProjectGetProjectsGet(page); // Use the generated client code and adapt it.
    // const getProject = (id: string) => new ProjectApi(config).apiProjectGetProjectProjectIdGetRaw({id});
    const addProject = (project: ProjectAddDTO) => new ProjectApi(config).apiProjectAddPost({ projectAddDTO: project });
    const editProject = (project: ProjectUpdateDTO) => new ProjectApi(config).apiProjectUpdatePut({ projectUpdateDTO: project });
    // const deleteProject = (id: string) => new ProjectApi(config).api({ id });

    return {
        getProjects: { // Return the query object.
            key: getProjectsQueryKey, // Add the key to identify the query.
            query: getProjects // Add the query callback.
        },
        // getProject: {
        //     key: getProjectQueryKey,
        //     query: getProject
        // },
        addProject: { // Return the mutation object.
            key: addProjectMutationKey, // Add the key to identify the mutation.
            mutation: addProject // Add the mutation callback.
        },
        editProject: { // Return the mutation object.
            key: editProjectMutationKey, // Add the key to identify the mutation.
            mutation: editProject // Add the mutation callback.
        },
        // deleteProject: {
        //     key: deleteProjectMutationKey,
        //     mutation: deleteProject
        // }
    }
}