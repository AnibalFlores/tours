import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  bgimage = '';
  bgfiles = [
    'bg-01.jpg',
    'bg-02.jpg',
    'bg-03.jpg',
    'bg-04.jpg',
    'bg-05.jpg',
    'bg-06.jpg',
    'bg-07.jpg',
    'bg-08.jpg',
    'bg-09.jpg',
  ];

  constructor() {
    const i = getRandomInt(0, this.bgfiles.length - 1);
    this.bgimage = this.bgfiles[i];
  }

  getParallaxStyles(): any {
    const styles = {
      overflow: 'hidden',
      'background-color': '#000',
      background: `linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 1.0)), url(./assets/images/${this.bgimage})`,
      'background-attachment': 'fixed',
      'background-repeat': 'no-repeat',
      'background-size': 'cover',
      'background-position': 'top center',
      animation: 'zoomin 12s ease-in-out infinite alternate',
      '-webkit-animation': 'zoomin 12s ease-in-out infinite alternate'
    };
    return styles;
  }

}

function getRandomInt(min: number, max: number): number {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

