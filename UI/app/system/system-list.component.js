System.register(['angular2/core'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1;
    var SystemListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            SystemListComponent = (function () {
                function SystemListComponent() {
                    this.componentTitle = 'SYSTEMS';
                    this.systems = [
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
                }
                SystemListComponent = __decorate([
                    core_1.Component({
                        selector: 'cc-system-list',
                        templateUrl: 'app/system/system-list.template.html'
                    }), 
                    __metadata('design:paramtypes', [])
                ], SystemListComponent);
                return SystemListComponent;
            }());
            exports_1("SystemListComponent", SystemListComponent);
        }
    }
});
//# sourceMappingURL=system-list.component.js.map