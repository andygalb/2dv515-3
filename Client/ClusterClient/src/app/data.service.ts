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

  getKMeans() {
    return this.http.get<Centroid[]>(this.url+'/api/cluster/kmeans', httpOptions);
  }

  getKMeansHalt() {
    return this.http.get<Centroid[]>(this.url+'/api/cluster/kmeanshalt', httpOptions);
  }

  getHierarchicalCluster() {
    return this.http.get<Cluster[]>(this.url+'/api/cluster/hierarchical', httpOptions);
  }

}

export class Centroid
{
  assignments: Assignment[];
  words: Word[];
}

export class Assignment
{
  title: String;
  words: Word[];
}

export class Word
{
  word: String;
  count: String;
}

export class Cluster
{
  left: Cluster;
  right: Cluster;
  parent: Cluster;
  blog: Blog;
  distance: String;
}

export class Blog
{
  title: String;
 // words: Word[];
}
