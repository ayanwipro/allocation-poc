param location string = resourceGroup().location
param searchName string

resource search 'Microsoft.Search/searchServices@2023-11-01' = {
  name: searchName
  location: location
  sku: {
    name: 'basic'
  }
}

output searchId string = search.id
