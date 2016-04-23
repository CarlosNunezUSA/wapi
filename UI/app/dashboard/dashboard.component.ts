import { Component } from 'angular2/core';

import { IDashboard } from './dashboard'

@Component({
    selector: 'cs-dashboard',
    templateUrl: 'app/dashboard/dashboard.template.html'
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