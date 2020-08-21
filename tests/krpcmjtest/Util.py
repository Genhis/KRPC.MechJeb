import re

__pattern = re.compile(r"(?<!^)(?=[A-Z])")

def toSnakeCase(value):
	return __pattern.sub("_", value).lower()
