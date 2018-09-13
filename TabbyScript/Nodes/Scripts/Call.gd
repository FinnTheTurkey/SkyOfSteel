extends "Base.gd"

const Type = 'exec'
var Call = ''

func execute():
	var args = []
	for child in get_children():
		var data = child.get_data()
		if data.type == Tabby.ERR:
			return data
		args.append(data)

	var returned = sroot.call_api(Call, args, self.line_number)

	if returned.type == Tabby.ERR:
		return returned

	return Tabby.malloc(Tabby.SUC)
