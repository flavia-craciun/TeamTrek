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
/**
 * 
 * @export
 * @interface AnswerAddDTO
 */
export interface AnswerAddDTO {
    /**
     * 
     * @type {string}
     * @memberof AnswerAddDTO
     */
    questionId?: string;
    /**
     * 
     * @type {string}
     * @memberof AnswerAddDTO
     */
    description?: string | null;
}

/**
 * Check if a given object implements the AnswerAddDTO interface.
 */
export function instanceOfAnswerAddDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function AnswerAddDTOFromJSON(json: any): AnswerAddDTO {
    return AnswerAddDTOFromJSONTyped(json, false);
}

export function AnswerAddDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): AnswerAddDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'questionId': !exists(json, 'questionId') ? undefined : json['questionId'],
        'description': !exists(json, 'description') ? undefined : json['description'],
    };
}

export function AnswerAddDTOToJSON(value?: AnswerAddDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'questionId': value.questionId,
        'description': value.description,
    };
}
