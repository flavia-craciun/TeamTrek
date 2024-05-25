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

import { exists, mapValues } from '../runtime';
import type { AnswerGetDTO } from './AnswerGetDTO';
import {
    AnswerGetDTOFromJSON,
    AnswerGetDTOFromJSONTyped,
    AnswerGetDTOToJSON,
} from './AnswerGetDTO';
import type { ErrorMessage } from './ErrorMessage';
import {
    ErrorMessageFromJSON,
    ErrorMessageFromJSONTyped,
    ErrorMessageToJSON,
} from './ErrorMessage';

/**
 * 
 * @export
 * @interface AnswerGetDTOListRequestResponse
 */
export interface AnswerGetDTOListRequestResponse {
    /**
     * 
     * @type {Array<AnswerGetDTO>}
     * @memberof AnswerGetDTOListRequestResponse
     */
    readonly response?: Array<AnswerGetDTO> | null;
    /**
     * 
     * @type {ErrorMessage}
     * @memberof AnswerGetDTOListRequestResponse
     */
    errorMessage?: ErrorMessage;
}

/**
 * Check if a given object implements the AnswerGetDTOListRequestResponse interface.
 */
export function instanceOfAnswerGetDTOListRequestResponse(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function AnswerGetDTOListRequestResponseFromJSON(json: any): AnswerGetDTOListRequestResponse {
    return AnswerGetDTOListRequestResponseFromJSONTyped(json, false);
}

export function AnswerGetDTOListRequestResponseFromJSONTyped(json: any, ignoreDiscriminator: boolean): AnswerGetDTOListRequestResponse {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'response': !exists(json, 'response') ? undefined : (json['response'] === null ? null : (json['response'] as Array<any>).map(AnswerGetDTOFromJSON)),
        'errorMessage': !exists(json, 'errorMessage') ? undefined : ErrorMessageFromJSON(json['errorMessage']),
    };
}

export function AnswerGetDTOListRequestResponseToJSON(value?: AnswerGetDTOListRequestResponse | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'errorMessage': ErrorMessageToJSON(value.errorMessage),
    };
}

