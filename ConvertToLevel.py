import sys
import json
from generated import types_pb2, level_pb2

def main():
	if len(sys.argv) < 2:
		print('python3 convert.py input.json output.level')
		return

	codLevelData = {}

	shapeMapping = {}
	shapeMapping["Cube"] = types_pb2.LevelNodeShape.CUBE
	shapeMapping["Sphere"] = types_pb2.LevelNodeShape.SPHERE
	shapeMapping["Cylinder"] = types_pb2.LevelNodeShape.CYLINDER
	shapeMapping["Pyramid"] = types_pb2.LevelNodeShape.PYRAMID
	shapeMapping["Prism"] = types_pb2.LevelNodeShape.PRISM
	shapeMapping["Sign"] = types_pb2.LevelNodeShape.SIGN

	typeMapping = {}
	typeMapping["DEFAULT"] = types_pb2.LevelNodeMaterial.DEFAULT
	typeMapping["DEFAULT_COLORED"] = types_pb2.LevelNodeMaterial.DEFAULT_COLORED
	typeMapping["GRABBABLE_CRUMBLING"] = types_pb2.LevelNodeMaterial.GRABBABLE_CRUMBLING
	typeMapping["Sign"] = types_pb2.LevelNodeMaterial.DEFAULT
	typeMapping["GRABBABLE"] = types_pb2.LevelNodeMaterial.GRABBABLE
	typeMapping["GRAPPLABLE"] = types_pb2.LevelNodeMaterial.GRAPPLABLE
	typeMapping["ICE"] = types_pb2.LevelNodeMaterial.ICE
	typeMapping["LAVA"] = types_pb2.LevelNodeMaterial.LAVA
	typeMapping["GRAPPLABLE_LAVA"] = types_pb2.LevelNodeMaterial.GRAPPLABLE_LAVA
	typeMapping["WOOD"] = types_pb2.LevelNodeMaterial.WOOD

	with open(sys.argv[1]) as inputFile:
		codLevelData = json.load(inputFile)

	level = level_pb2.Level()
	level.formatVersion = 4
	if "title" in codLevelData:
		level.title = codLevelData["title"]
	if "creators" in codLevelData:
		level.creators = codLevelData["creators"]
	if "description" in codLevelData:
		level.description = codLevelData["description"]
	if "checkpoints" in codLevelData:
		level.maxCheckpointCount = codLevelData["checkpoints"]
	if "complexity" in codLevelData:
		level.complexity = codLevelData["complexity"]

	if "start" in codLevelData:
		node = level.levelNodes.add()
		node.levelNodeStart.position.x = codLevelData["start"]["position"][0]
		node.levelNodeStart.position.y = codLevelData["start"]["position"][1]
		node.levelNodeStart.position.z = codLevelData["start"]["position"][2]
		node.levelNodeStart.rotation.x = codLevelData["start"]["rotation"][0]
		node.levelNodeStart.rotation.y = codLevelData["start"]["rotation"][1]
		node.levelNodeStart.rotation.z = codLevelData["start"]["rotation"][2]
		node.levelNodeStart.rotation.w = codLevelData["start"]["rotation"][3]
		node.levelNodeStart.radius = codLevelData["start"]["radius"]

	if "finish" in codLevelData:
		node = level.levelNodes.add()
		node.levelNodeFinish.position.x = codLevelData["finish"]["position"][0]
		node.levelNodeFinish.position.y = codLevelData["finish"]["position"][1]
		node.levelNodeFinish.position.z = codLevelData["finish"]["position"][2]
		node.levelNodeFinish.radius = codLevelData["finish"]["radius"]

	for element in codLevelData["nodes"]:
		node = level.levelNodes.add()		
		if shapeMapping[element["shape"]] == types_pb2.LevelNodeShape.SIGN:
			node.levelNodeSign.position.x = element["position"][0]
			node.levelNodeSign.position.y = element["position"][1]
			node.levelNodeSign.position.z = element["position"][2]
			node.levelNodeSign.rotation.x = element["rotation"][0]
			node.levelNodeSign.rotation.y = element["rotation"][1]
			node.levelNodeSign.rotation.z = element["rotation"][2]
			node.levelNodeSign.rotation.w = element["rotation"][3]
			node.levelNodeSign.text = element["text"]

		else:
			node.levelNodeStatic.shape = shapeMapping[element["shape"]]
			node.levelNodeStatic.material = typeMapping[element["type"]]
			node.levelNodeStatic.position.x = element["position"][0]
			node.levelNodeStatic.position.y = element["position"][1]
			node.levelNodeStatic.position.z = element["position"][2]
			node.levelNodeStatic.rotation.x = element["rotation"][0]
			node.levelNodeStatic.rotation.y = element["rotation"][1]
			node.levelNodeStatic.rotation.z = element["rotation"][2]
			node.levelNodeStatic.rotation.w = element["rotation"][3]
			
			if "scale" in element:
				node.levelNodeStatic.scale.x = element["scale"][0]
				node.levelNodeStatic.scale.y = element["scale"][1]
				node.levelNodeStatic.scale.z = element["scale"][2]

			if typeMapping[element["type"]] == types_pb2.LevelNodeMaterial.DEFAULT_COLORED:
				node.levelNodeStatic.color.r = element["color"][0]
				node.levelNodeStatic.color.g = element["color"][1]
				node.levelNodeStatic.color.b = element["color"][2]
				node.levelNodeStatic.color.a = 1.0
				
			if typeMapping[element["type"]] == types_pb2.LevelNodeMaterial.GRABBABLE_CRUMBLING:
				node.levelNodeCrumbling.shape = shapeMapping[element["shape"]]
				node.levelNodeCrumbling.material = typeMapping[element["type"]]
				node.levelNodeCrumbling.position.x = element["position"][0]
				node.levelNodeCrumbling.position.y = element["position"][1]
				node.levelNodeCrumbling.position.z = element["position"][2]
				node.levelNodeCrumbling.scale.x = element["scale"][0]
				node.levelNodeCrumbling.scale.y = element["scale"][1]
				node.levelNodeCrumbling.scale.z = element["scale"][2]
				node.levelNodeCrumbling.rotation.x = element["rotation"][0]
				node.levelNodeCrumbling.rotation.y = element["rotation"][1]
				node.levelNodeCrumbling.rotation.z = element["rotation"][2]
				node.levelNodeCrumbling.rotation.w = element["rotation"][3]
				#if element stable_time is there, set it
				if "stable_time" in element:
					node.levelNodeCrumbling.stableTime = element["stable_time"]
					node.levelNodeCrumbling.respawnTime = element["respawn_time"]

	with open(sys.argv[2], "wb") as outputFile:
		outputFile.write(level.SerializeToString())


if __name__ == '__main__':
	main()
