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
import type { ProjectDTO } from './ProjectDTO';
import {
    ProjectDTOFromJSON,
    ProjectDTOFromJSONTyped,
    ProjectDTOToJSON,
} from './ProjectDTO';

/**
 * 
 * @export
 * @interface ProjectDTOPagedResponse
 */
export interface ProjectDTOPagedResponse {
    /**
     * 
     * @type {number}
     * @memberof ProjectDTOPagedResponse
     */
    page?: number;
    /**
     * 
     * @type {number}
     * @memberof ProjectDTOPagedResponse
     */
    pageSize?: number;
    /**
     * 
     * @type {number}
     * @memberof ProjectDTOPagedResponse
     */
    totalCount?: number;
    /**
     * 
     * @type {Array<ProjectDTO>}
     * @memberof ProjectDTOPagedResponse
     */
    data?: Array<ProjectDTO> | null;
}

/**
 * Check if a given object implements the ProjectDTOPagedResponse interface.
 */
export function instanceOfProjectDTOPagedResponse(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function ProjectDTOPagedResponseFromJSON(json: any): ProjectDTOPagedResponse {
    return ProjectDTOPagedResponseFromJSONTyped(json, false);
}

export function ProjectDTOPagedResponseFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProjectDTOPagedResponse {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'page': !exists(json, 'page') ? undefined : json['page'],
        'pageSize': !exists(json, 'pageSize') ? undefined : json['pageSize'],
        'totalCount': !exists(json, 'totalCount') ? undefined : json['totalCount'],
        'data': !exists(json, 'data') ? undefined : (json['data'] === null ? null : (json['data'] as Array<any>).map(ProjectDTOFromJSON)),
    };
}

export function ProjectDTOPagedResponseToJSON(value?: ProjectDTOPagedResponse | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'page': value.page,
        'pageSize': value.pageSize,
        'totalCount': value.totalCount,
        'data': value.data === undefined ? undefined : (value.data === null ? null : (value.data as Array<any>).map(ProjectDTOToJSON)),
    };
}

