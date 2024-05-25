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
import type { ErrorMessage } from './ErrorMessage';
import {
    ErrorMessageFromJSON,
    ErrorMessageFromJSONTyped,
    ErrorMessageToJSON,
} from './ErrorMessage';
import type { MemberDTO } from './MemberDTO';
import {
    MemberDTOFromJSON,
    MemberDTOFromJSONTyped,
    MemberDTOToJSON,
} from './MemberDTO';

/**
 * 
 * @export
 * @interface MemberDTOListRequestResponse
 */
export interface MemberDTOListRequestResponse {
    /**
     * 
     * @type {Array<MemberDTO>}
     * @memberof MemberDTOListRequestResponse
     */
    readonly response?: Array<MemberDTO> | null;
    /**
     * 
     * @type {ErrorMessage}
     * @memberof MemberDTOListRequestResponse
     */
    errorMessage?: ErrorMessage;
}

/**
 * Check if a given object implements the MemberDTOListRequestResponse interface.
 */
export function instanceOfMemberDTOListRequestResponse(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function MemberDTOListRequestResponseFromJSON(json: any): MemberDTOListRequestResponse {
    return MemberDTOListRequestResponseFromJSONTyped(json, false);
}

export function MemberDTOListRequestResponseFromJSONTyped(json: any, ignoreDiscriminator: boolean): MemberDTOListRequestResponse {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'response': !exists(json, 'response') ? undefined : (json['response'] === null ? null : (json['response'] as Array<any>).map(MemberDTOFromJSON)),
        'errorMessage': !exists(json, 'errorMessage') ? undefined : ErrorMessageFromJSON(json['errorMessage']),
    };
}

export function MemberDTOListRequestResponseToJSON(value?: MemberDTOListRequestResponse | null): any {
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
