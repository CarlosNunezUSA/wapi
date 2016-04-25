import { Component } from 'angular2/core';

import { IDashboard } from './dashboard'

import { SystemListComponent } from '../system/system-list.component'
 
@Component({
    selector: 'cc-dashboard',
    templateUrl: 'app/dashboard/dashboard.template.html',
    directives: [SystemListComponent]
})
export class DashboardComponent {
    pageTitle: string = 'Dashboard';
    systems: any[] = [
        {
            "name": "Mobile Dev",
            "lastRun": "1/1/2015"
        },
        {
            "name": "LDAP Loader",
            "lastRun": "1/1/2015"
        },
        {
            "name": "LDAP Loader",
            "lastRun": "1/1/2015"
        },
        {
            "name": "LDAP Loader",
            "lastRun": "1/1/2015"
        }

    ];

}