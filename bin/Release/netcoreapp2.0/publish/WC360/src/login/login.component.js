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
var auth_service_1 = require("../authservice/auth.service");
var router_1 = require("@angular/router");
var LoginComponent = /** @class */ (function () {
    function LoginComponent(auth, router, http) {
        this.auth = auth;
        this.router = router;
        this.http = http;
        this.model = new LoginModel();
        if (auth.isAuthenticated()) {
            router.navigateByUrl('/home');
        }
    }
    LoginComponent.prototype.login = function () {
        var _this = this;
        this.http.post('http://localhost:2323/api/token/', this.model, { headers: { 'Content-Type': 'application/json; charset=utf-8' } })
            .subscribe(function (data) { return _this.loginSuccess(data); }, function (error) { return console.log('unauthorized!'); });
    };
    LoginComponent.prototype.loginSuccess = function (token) {
        localStorage.setItem('token', token.token);
        localStorage.setItem('user', this.model.userName);
        this.router.navigateByUrl('/home');
    };
    LoginComponent = __decorate([
        core_1.Component({
            // tslint:disable-next-line:component-selector
            selector: 'login',
            templateUrl: './login.component.html',
            styleUrls: ['./login.component.css'],
            providers: [auth_service_1.AuthService]
        }),
        __metadata("design:paramtypes", [auth_service_1.AuthService, router_1.Router, http_1.HttpClient])
    ], LoginComponent);
    return LoginComponent;
}());
exports.LoginComponent = LoginComponent;
var LoginModel = /** @class */ (function () {
    function LoginModel() {
    }
    return LoginModel;
}());
//# sourceMappingURL=login.component.js.map