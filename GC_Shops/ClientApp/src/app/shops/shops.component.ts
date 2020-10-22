import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-shops',
  templateUrl: './shops.component.html',
  styleUrls: ['./shops.component.css']
})
export class ShopsComponent {
  public shops: Shop[]
  public shopItems: Item[]
  private httpClient: HttpClient
  private baseUrl: string
  private ItemState = ItemState

  public get visibleShops() { return this.shops.filter(s => s.show) }

  constructor(httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = httpClient
    this.baseUrl = baseUrl
    httpClient.get<Shop[]>(`${baseUrl}api/shops`)
      .subscribe(result => {
        this.shops = result
          .sort((a, b) => a.name == b.name ? 0 : a.name < b.name ? -1 : 1)
          .map(shop => ({
            ...shop,
            show: true
          }))

        httpClient.get<Item[]>(`${baseUrl}api/items`)
          .subscribe(result => {
            const shopItems = []
            result.forEach(item => {
              item.shops = {}
              this.shops.forEach(shop => {
                var shopItem = shop.items.find(i => i.id == item.id)
                if (shopItem == null) {
                  shopItem = {
                    id: item.id,
                    state: ItemState.unknown,
                    sellCost: 0,
                    itemsInSell: 0,
                    buyCost: 0,
                    itemsInBuy: 0
                  }
                } else {
                  shopItem.state = ItemState.unmodified
                }
                item.shops[shop.id] = shopItem
              })
              shopItems.push(item)
            })
            this.shopItems = shopItems
          }, error => console.error(error))
      }, error => console.error(error));
  }

  public IsBestCost(shopItem: Item, isBuy: boolean, id: string): boolean {
    let bestShop: string = undefined
    let bestCost: number = undefined
    this.shops.forEach(shop => {
      if (!shop.show) {
        return
      }
      let cost: number, count: number
      let item = shopItem.shops[shop.id]
      if (isBuy) {
        cost = item.buyCost
        count = item.itemsInBuy
      } else {
        cost = item.sellCost
        count = item.itemsInSell
      }
      if (count <= 0 || cost <= 0) {
        return
      }
      const currentCost = cost / count
      if (currentCost == bestCost) {
        if (bestShop != id) {
          bestShop = id
        }
      } else {
        if (bestShop == undefined
          || isBuy && bestCost > currentCost
          || !isBuy && bestCost < currentCost) {
          bestShop = shop.id
          bestCost = currentCost
        }
      }
    })
    return bestShop == id
  }

  public onChange(shopItem: ShopItem) {
    if (shopItem.state == ItemState.unknown) {
      shopItem.state = ItemState.created
    } else if (shopItem.state == ItemState.unmodified) {
      shopItem.state = ItemState.modified
    }
  }

  public SaveChanges() {
    this.shopItems.forEach(item => {
      this.shops.forEach(shop => {
        const shopItem = item.shops[shop.id]
        if (shopItem.state == ItemState.modified || shopItem.state == ItemState.created) {
          this.httpClient.post(`${this.baseUrl}api/shops/${shop.id}/items/${shopItem.id}`, {
            sellCost: parseFloat(`${shopItem.sellCost}`),
            itemsInSell: parseFloat(`${shopItem.itemsInSell}`),
            buyCost: parseFloat(`${shopItem.buyCost}`),
            itemsInBuy: parseFloat(`${shopItem.itemsInBuy}`)
          })
            .subscribe(() => {
              shopItem.state = ItemState.unknown
            }, error => {
              console.info(shop.name, shopItem.id)
              console.error(error)
            })
        }
      })
    })
  }

  public RoundValue(value: number): number {
    return Math.ceil(value * 100) / 100
  }
}

enum ItemState {
  unknown = 0,
  unmodified = 1,
  created = 2,
  modified = 3
}

interface Item {
  id: string
  name: string
  stackSize: number
  shops: { [key: string]: ShopItem }
}
interface Shop {
  id: string
  name: string
  items: ShopItem[]
  show: boolean
}

interface ShopItem {
  id: string
  sellCost: number
  itemsInSell: number
  buyCost: number
  itemsInBuy: number
  state: ItemState
}
