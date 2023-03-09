import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'
import { Users } from '../models/users';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl : string = "https://localhost:7191/api/User/"
  constructor(private http : HttpClient) { }

  GetUser() : Observable<Users[]>{
    return this.http.get<Users[]>(this.baseUrl)
  }

  DeleteUser(id: string) : Observable<Users>{
    return this.http.delete<Users>(this.baseUrl + id)
  }


  signup(userObj:any){
    return this.http.post<any>(`${this.baseUrl}register`,userObj)
  }

  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}authenticate`,loginObj)
  }
}
