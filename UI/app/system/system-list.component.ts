import { Component } from 'angular2/core';

import { ISystem } from './system'

@Component({
    selector: 'cc-system-list',
    templateUrl: 'app/system/system-list.template.html'
})
export class SystemListComponent {
    componentTitle: string = 'SYSTEMS';
    systems: ISystem[] = [
        {
            "id": 1,
            "name": "System 1",
            "isActive": true
        },
        {
            "id": 2,
            "name": "System 2",
            "isActive": true
        }
    ];

    // methods


}