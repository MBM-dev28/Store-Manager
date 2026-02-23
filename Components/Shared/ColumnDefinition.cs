namespace Store_Manager.Components.Shared;

public record ColumnDefinition<TItem>(string Header, Func<TItem, string> Value); //TItem placeholder for type we provide (in my case here its for my viewmodels)
