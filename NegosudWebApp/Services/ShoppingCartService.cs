using Blazored.LocalStorage;
using NegosudWebApp.Models;


namespace NegosudWebApp.Services;

public class ShoppingCartService
{
    private readonly ILocalStorageService _localStorage;
    public ShoppingCart Cart { get; private set; } = new ShoppingCart();

    public ShoppingCartService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task LoadCartAsync()
    {
        var cart = await _localStorage.GetItemAsync<ShoppingCart>("shoppingCart");
        if (cart != null)
        {
            Cart = cart;
        }
    }

    public async Task SaveCartAsync()
    {
        await _localStorage.SetItemAsync("shoppingCart", Cart);
    }

    public async Task ClearCartAsync()
    {
        Cart.Items.Clear();
        await _localStorage.RemoveItemAsync("shoppingCart");
    }

    public async Task AddToCartAsync(CartItem item)
    {
        var existingItem = Cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            Cart.Items.Add(item);
        }

        await SaveCartAsync();
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var item = Cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            Cart.Items.Remove(item);
        }

        await SaveCartAsync();
    }
}
