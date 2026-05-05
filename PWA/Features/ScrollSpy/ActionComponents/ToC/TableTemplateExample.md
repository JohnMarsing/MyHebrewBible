
```html
	<TableTemplate Items="@Data"
					HeaderCSS="table table-sm table-hover table-bordered"
					TheadCSS="table-dark">
		<TableHeader>
			<th class="text-center">Verse</th>
			<th>Outline</th>
		</TableHeader>
		<RowTemplate>
			<td class="text-center">
				<button class="btn btn-link p-0" title="Go to verse @context.Verse (@context.ID)"
					@onclick="() => OnSelected.InvokeAsync(new ReturnedRecord(context.Verse, context.ID))">
					@context.Verse
					@if (context.VerseOffset != "NULL")
					{
						<sup><span class="fst-italic fw-light">&nbsp;@context.VerseOffset</span></sup>
					}
				</button>
			</td>
			<td class="fst-italic">@context.Description</td>
		</RowTemplate>
	</TableTemplate>

```