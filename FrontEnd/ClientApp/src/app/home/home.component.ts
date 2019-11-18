import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public employeeeSalaries: EmployeeSalary[];
  public employeeIds = "";


  constructor(private http: HttpClient) { } 


  onQuerySalaries(event) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

   const data = this.employeeIds.split(',');


    this.http.post('https://localhost:44311/api/Salary', data, httpOptions).subscribe(data => {
      this.employeeeSalaries = ((data) as EmployeeSalary[]);
    });
  }


  formatCurrency(number) {
    const formatter = new Intl.NumberFormat('en-US',
      {
        style: 'currency',
        currency: 'USD',
        minimumFractionDigits: 2
      });

    return formatter.format(number); // "$1,000.00"

  }


}

class EmployeeSalary {
  id: number;
  name: string;
  salary: number;

}
