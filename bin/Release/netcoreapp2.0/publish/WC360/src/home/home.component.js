"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var HomeComponent = /** @class */ (function () {
    function HomeComponent(http) {
        this.http = http;
        this.rounds = [];
    }
    // tslint:disable-next-line:use-life-cycle-interface
    HomeComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.http.get('http://localhost:2323/api/round').subscribe(function (data) { return _this.rounds = data; });
    };
    HomeComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'home',
            templateUrl: './home.component.html',
            styles: ['./home.component.css']
        }),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
//# sourceMappingURL=home.component.js.map