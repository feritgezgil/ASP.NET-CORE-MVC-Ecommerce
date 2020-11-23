﻿var cities = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('text'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    prefetch: 'assets/cities.json'
});
cities.initialize();

var elt = $('input');
elt.tagsinput({
    itemValue: 'value',
    itemText: 'text',
    typeaheadjs: {
        name: 'cities',
        displayKey: 'text',
        source: cities.ttAdapter()
    }
});