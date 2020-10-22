import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css']
})
export class ItemsComponent {
  public items: Item[]
  private httpClient: HttpClient
  private baseUrl: string
  private ItemState = ItemState

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = httpClient
    this.baseUrl = baseUrl
    httpClient.get<Item[]>(`${baseUrl}api/items`)
      .subscribe(result => {
        this.items = result.map(r => ({
          ...r,
          state: ItemState.unmodified
        }));
      }, error => console.error(error));
  }

  public AddItem() {
    this.items.push({
      id: '',
      name: '',
      stackSize: 64,
      state: ItemState.created
    })
  }

  public ModifyItem(id: string) {
    const item = this.items.find(i => i.id == id);
    if (item.state == ItemState.unmodified) {
      item.state = ItemState.modified
    }
  }

  public SaveItems() {
    const addedItems = this.items.filter(i => i.state === ItemState.created);
    const modifiedItems = this.items.filter(i => i.state === ItemState.modified);
    addedItems.forEach(item => {
      this.httpClient.post(`${this.baseUrl}api/items`, {
        id: item.id,
        name: item.name,
        stackSize: parseInt(item.stackSize + '')
      })
        .subscribe(() => {
          item.state = ItemState.unmodified
        }, error => console.error(error));
    })
    modifiedItems.forEach(item => {
      this.httpClient.put(`${this.baseUrl}api/items/${item.id}`, {
        name: item.name,
        stackSize: item.stackSize
      })
        .subscribe(() => {
          item.state = ItemState.unmodified
        }, error => console.error(error));
    })
  }
}

enum ItemState {
  unmodified = 0,
  created = 1,
  modified = 2
}

interface Item {
  id: string
  name: string
  stackSize: number
  state: ItemState
}
