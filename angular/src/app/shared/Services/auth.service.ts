import { TokenStorageService } from 'src/app/shared/Services/token.service';
import { Injectable } from "@angular/core"; 
import { Observable } from "rxjs";
import { LoginRequestDto } from "../Models/login-request.dto";
import { LoginResponseDto } from "../Models/login-response.dto";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { ACCESS_TOKEN, REFRESH_TOKEN } from "../Constants/keys.constant";

@Injectable({
    providedIn: 'root',
})

export class AuthService {

    constructor(private httpClient : HttpClient, private tokenService : TokenStorageService){}

    public login(input : LoginRequestDto) : Observable<LoginResponseDto>{
        var body = {
            username: input.username,
            password: input.password,
            client_id: environment.oAuthConfig.clientId,
            client_secret: environment.oAuthConfig.dummyClientSecret,
            grant_type: 'password',
            scope: environment.oAuthConfig.scope
        };

        const data = Object.keys(body).map((key, index) => `${key}=${encodeURIComponent(body[key])}`).join('&');

        return this.httpClient.post<LoginResponseDto>(
            environment.oAuthConfig.issuer + 'connect/token',
            data,
            {
                headers: { 'Content-Type' : 'application/x-www-form-urlencoded' }
            }
        )
    }

    public refreshToken(refreshToken : string) : Observable<LoginResponseDto>{
        var body = {
            // username: input.username,
            // password: input.password,
            client_id: environment.oAuthConfig.clientId,
            client_secret: environment.oAuthConfig.dummyClientSecret,
            grant_type: 'refresh_token',
            // scope: environment.oAuthConfig.scope
            refresh_token: refreshToken
        };

        const data = Object.keys(body).map((key, index) => `${key}=${encodeURIComponent(body[key])}`).join('&');

        return this.httpClient.post<LoginResponseDto>(
            environment.oAuthConfig.issuer + 'connect/token',
            data,
            {
                headers: { 'Content-Type' : 'application/x-www-form-urlencoded' }
            }
        )
    }

    //lấy token ra
    public isAuthenticated() : boolean
    {
        return this.tokenService.getToken() != null
    }

    public logout()
    {
        this.tokenService.signOut();
    }
}