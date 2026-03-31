import pluralize from 'pluralize';

export function belongsToIndex({ source, destination, type }, req, data, GetDestinationArray) {
    if (type !== 'belongsTo') return data;
    if (req.url !== `/${source}`) return data;
    if (req.method !== "GET") return data;
    for (const item of data) {
        SetSingularItem(item, destination, GetDestinationArray(destination).find(a => item[`${pluralize.singular(destination)}_id`] == a.id))
        delete item[`${pluralize.singular(destination)}_id`]
    }
    return data;
}

export function belongsToSingle({ source, destination, type }, req, data, GetDestinationArray) {
    if (type !== 'belongsTo') return data;
    if (!req.url.includes(`/${source}`)) return data;
    if (["GET", "POST", "PUT"].includes(req.method)) {
        SetSingularItem(data, destination, GetDestinationArray(destination).filter(a => data[`${pluralize.singular(destination)}_id`] == a.id))
        return data;
    }
}

export function hasManyIndex({ source, destination, type }, req, data, GetDestinationArray) {
    if (type !== 'hasMany') return data;
    if (req.url !== `/${source}`) return data;
    if (req.method !== "GET") return data;
    for (const item of data) {
        SetPluralItem(item, destination, Filter(GetDestinationArray(destination), item, `${pluralize.singular(source)}_id`, 'id'))
    }
    return data;
}

export function hasManySingle({ source, destination, type }, req, data, GetDestinationArray) {
    if (type !== 'hasMany') return data;
    if (!req.url.includes(`/${source}`)) return data;
    if (["GET", "POST", "PUT"].includes(req.method)) {
        SetPluralItem(data, destination, Filter(GetDestinationArray(destination), data, `${pluralize.singular(source)}_id`, 'id'))
    }
    return data;
}

export const SetSingularItem = (item, destination, value) => item[pluralize.singular(destination)] = value
export const SetPluralItem = (item, destination, value) => item[destination] = value
export const Filter = (array, src, foreignKey, primaryKey) => array.filter(dest => parseInt(dest[foreignKey]) === parseInt(src[primaryKey]))
