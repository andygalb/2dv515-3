import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Access-Control-Allow-Origin': '*',
  })
};

@Injectable({
  providedIn: 'root'
})

export class DataService {


  url = 'http://localhost:45996';

  constructor(private http: HttpClient) { }

  getSearch(query: String) {
    return this.http.get<Result[]>(this.url+'/api/search/'+query, httpOptions);
  }

  getSearchFrequency(query: String) {
    return this.http.get<Result[]>(this.url + '/api/search/frequency/'+ query, httpOptions);
  }

  getSearchFrequencyLocation(query: String) {
    return this.http.get<Result[]>(this.url+'/api/search/frequencylocation/'+ query, httpOptions);
  }

}

export class Result
{
    url: string;
    score: number;
    contentScore: number;
    locationScore: number;
    pageRankScore: number;
}

