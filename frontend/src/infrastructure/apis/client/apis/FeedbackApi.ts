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
  FeedbackAddDTO,
  FeedbackDTORequestResponse,
  FeedbackUpdateDTO,
  RequestResponse,
} from '../models';
import {
    FeedbackAddDTOFromJSON,
    FeedbackAddDTOToJSON,
    FeedbackDTORequestResponseFromJSON,
    FeedbackDTORequestResponseToJSON,
    FeedbackUpdateDTOFromJSON,
    FeedbackUpdateDTOToJSON,
    RequestResponseFromJSON,
    RequestResponseToJSON,
} from '../models';

export interface ApiFeedbackAddPostRequest {
    feedbackAddDTO?: FeedbackAddDTO;
}

export interface ApiFeedbackDeleteFeedbackIdDeleteRequest {
    feedbackId: string;
}

export interface ApiFeedbackGetFeedbackFeedbackIdGetRequest {
    feedbackId: string;
}

export interface ApiFeedbackUpdatePutRequest {
    feedbackUpdateDTO?: FeedbackUpdateDTO;
}

/**
 * 
 */
export class FeedbackApi extends runtime.BaseAPI {

    /**
     */
    async apiFeedbackAddPostRaw(requestParameters: ApiFeedbackAddPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Feedback/Add`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: FeedbackAddDTOToJSON(requestParameters.feedbackAddDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiFeedbackAddPost(requestParameters: ApiFeedbackAddPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiFeedbackAddPostRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiFeedbackDeleteFeedbackIdDeleteRaw(requestParameters: ApiFeedbackDeleteFeedbackIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        if (requestParameters.feedbackId === null || requestParameters.feedbackId === undefined) {
            throw new runtime.RequiredError('feedbackId','Required parameter requestParameters.feedbackId was null or undefined when calling apiFeedbackDeleteFeedbackIdDelete.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Feedback/Delete/{feedbackId}`.replace(`{${"feedbackId"}}`, encodeURIComponent(String(requestParameters.feedbackId))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiFeedbackDeleteFeedbackIdDelete(requestParameters: ApiFeedbackDeleteFeedbackIdDeleteRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiFeedbackDeleteFeedbackIdDeleteRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiFeedbackGetFeedbackFeedbackIdGetRaw(requestParameters: ApiFeedbackGetFeedbackFeedbackIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<FeedbackDTORequestResponse>> {
        if (requestParameters.feedbackId === null || requestParameters.feedbackId === undefined) {
            throw new runtime.RequiredError('feedbackId','Required parameter requestParameters.feedbackId was null or undefined when calling apiFeedbackGetFeedbackFeedbackIdGet.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Feedback/GetFeedback/{feedbackId}`.replace(`{${"feedbackId"}}`, encodeURIComponent(String(requestParameters.feedbackId))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => FeedbackDTORequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiFeedbackGetFeedbackFeedbackIdGet(requestParameters: ApiFeedbackGetFeedbackFeedbackIdGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<FeedbackDTORequestResponse> {
        const response = await this.apiFeedbackGetFeedbackFeedbackIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiFeedbackUpdatePutRaw(requestParameters: ApiFeedbackUpdatePutRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<RequestResponse>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.apiKey) {
            headerParameters["Authorization"] = this.configuration.apiKey("Authorization"); // Bearer authentication
        }

        const response = await this.request({
            path: `/api/Feedback/Update`,
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: FeedbackUpdateDTOToJSON(requestParameters.feedbackUpdateDTO),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => RequestResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiFeedbackUpdatePut(requestParameters: ApiFeedbackUpdatePutRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<RequestResponse> {
        const response = await this.apiFeedbackUpdatePutRaw(requestParameters, initOverrides);
        return await response.value();
    }

}