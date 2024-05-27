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
 * @interface FeedbackAddDTO
 */
export interface FeedbackAddDTO {
    /**
     * 
     * @type {number}
     * @memberof FeedbackAddDTO
     */
    rating?: number;
    /**
     * 
     * @type {string}
     * @memberof FeedbackAddDTO
     */
    frequentedSection?: string | null;
    /**
     * 
     * @type {string}
     * @memberof FeedbackAddDTO
     */
    suggestion?: string | null;
    /**
     * 
     * @type {number}
     * @memberof FeedbackAddDTO
     */
    responseWanted?: number;
}

/**
 * Check if a given object implements the FeedbackAddDTO interface.
 */
export function instanceOfFeedbackAddDTO(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function FeedbackAddDTOFromJSON(json: any): FeedbackAddDTO {
    return FeedbackAddDTOFromJSONTyped(json, false);
}

export function FeedbackAddDTOFromJSONTyped(json: any, ignoreDiscriminator: boolean): FeedbackAddDTO {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'rating': !exists(json, 'rating') ? undefined : json['rating'],
        'frequentedSection': !exists(json, 'frequentedSection') ? undefined : json['frequentedSection'],
        'suggestion': !exists(json, 'suggestion') ? undefined : json['suggestion'],
        'responseWanted': !exists(json, 'responseWanted') ? undefined : json['responseWanted'],
    };
}

export function FeedbackAddDTOToJSON(value?: FeedbackAddDTO | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'rating': value.rating,
        'frequentedSection': value.frequentedSection,
        'suggestion': value.suggestion,
        'responseWanted': value.responseWanted,
    };
}

