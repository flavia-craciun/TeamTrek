/* tslint:disable */
/* eslint-disable */
/**
 * MobyLab Web App
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  ProjectTaskDTORequestResponse,
  ProjectTaskUpdateDTO,
  RequestResponse,
  TaskAddDTO,
} from '../models';
import {
    ProjectTaskDTORequestResponseFromJSON,
    ProjectTaskDTORequestResponseToJSON,
    ProjectTaskUpdateDTOFromJSON,
    ProjectTaskUpdateDTOToJSON,
    RequestResponseFromJSON,
    RequestResponseToJSON,
    TaskAddDTOFromJSON,
    TaskAddDTOToJSON,
} from '../models';

export interface ApiProjectTaskAddPostRequest {
    taskAddDTO?: TaskAddDTO;
}

export interface ApiProjectTaskDeleteTaskIdDeleteRequest {
    taskId: string;
}

export interface ApiProjectTaskGetProjectTaskTaskIdGetRequest {
    taskId: string;
}

export interface ApiProjectTaskUpdatePutRequest {
    projectTaskUpdateDTO?: ProjectTaskUpdateDTO;
}

/**
 * 
 */
export class ProjectTaskApi extends runtime.BaseAPI {

    /**
     */
    async apiProjectTaskAddPostRaw(requestParameters: ApiProjectTaskAddPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/ProjectTask/Add`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: TaskAddDTOToJSON(requestParameters.taskAddDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProjectTaskAddPost(requestParameters: ApiProjectTaskAddPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiProjectTaskAddPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProjectTaskDeleteTaskIdDeleteRaw(requestParameters: ApiProjectTaskDeleteTaskIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        if (requestParameters.taskId === null || requestParameters.taskId === undefined) {
            throw new runtime.RequiredError('taskId','Required parameter requestParameters.taskId was null or undefined when calling apiProjectTaskDeleteTaskIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/ProjectTask/Delete/{taskId}`.replace(`{${"taskId"}}`, encodeURIComponent(String(requestParameters.taskId))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProjectTaskDeleteTaskIdDelete(requestParameters: ApiProjectTaskDeleteTaskIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiProjectTaskDeleteTaskIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProjectTaskGetProjectTaskTaskIdGetRaw(requestParameters: ApiProjectTaskGetProjectTaskTaskIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<ProjectTaskDTORequestResponse>> {
        if (requestParameters.taskId === null || requestParameters.taskId === undefined) {
            throw new runtime.RequiredError('taskId','Required parameter requestParameters.taskId was null or undefined when calling apiProjectTaskGetProjectTaskTaskIdGet.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/ProjectTask/GetProjectTask/{taskId}`.replace(`{${"taskId"}}`, encodeURIComponent(String(requestParameters.taskId))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => ProjectTaskDTORequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProjectTaskGetProjectTaskTaskIdGet(requestParameters: ApiProjectTaskGetProjectTaskTaskIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<ProjectTaskDTORequestResponse> {
        const response = await this.apiProjectTaskGetProjectTaskTaskIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiProjectTaskUpdatePutRaw(requestParameters: ApiProjectTaskUpdatePutRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/ProjectTask/Update`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: ProjectTaskUpdateDTOToJSON(requestParameters.projectTaskUpdateDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiProjectTaskUpdatePut(requestParameters: ApiProjectTaskUpdatePutRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiProjectTaskUpdatePutRaw(requestParameters, initOverrides);
        return await response.value();
    }

}
